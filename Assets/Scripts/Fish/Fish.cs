using UnityEngine;

namespace Fish
{
    [RequireComponent (typeof(CapsuleCollider2D))]
    public class Fish : MonoBehaviour
    {
        public FishData data;
        public bool faceLeft;

        public bool goingUp;
        public bool canChase;
        public float continueDirection;

        public Transform target;
        CapsuleCollider2D detector;
        SpriteRenderer renderer;
        public void Init(FishData _data, Vector2 position, bool left)
        {
            data = _data;
            transform.position = position;
            faceLeft = left;
            target = null;

            GameObject gb = new();
            gb.transform.parent = transform;
            gb.transform.localPosition = new();


            detector = gb.AddComponent<CapsuleCollider2D>();
            detector.direction = CapsuleDirection2D.Horizontal;
            detector.offset = new Vector2(-(0.65f + (data.seeRange/2)), 0);
            detector.size = new Vector2(data.seeRange, 0.25f);
            detector.isTrigger = true;

            CapsuleCollider2D body = gameObject.GetComponent<CapsuleCollider2D>();
            body.direction = CapsuleDirection2D.Horizontal;
            body.size = new(0.65f, 0.25f);

            Rigidbody2D rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;


            renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = data.sprite;

            canChase = true;
            Turn(faceLeft);
            gameObject.layer = LayerMask.NameToLayer("Fish");
        }

        public void Turn(bool toggle = true)
        {
            if(toggle)
                faceLeft = !faceLeft;
            renderer.flipX = !faceLeft;
            detector.offset = new Vector2((faceLeft ? -1 : 1) * (0.65f + (data.seeRange / 2)), 0);
        }

        public void TurnOff()
        {
            canChase = false;
            target = null;
            detector.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Turn();
                Debug.Log("hit a wall");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hook"))
            {
                if(collision.GetComponent<HookControls>().BaitLevel == data.level)
                {

                    target = collision.transform;
                    Debug.Log("found bait!");
                }
                else
                {
                    Debug.Log("Ewww!");
                }
            }
        }

        public void Wander(FishSpawner spawner)
        {
            Vector2 moveVec = faceLeft ? Vector2.left : Vector2.right;
            if (continueDirection > 0)
            {
                moveVec.y = (goingUp ? 0.2f : -0.2f);
                continueDirection -= Time.deltaTime;
            }
            else
            {
                continueDirection = Random.Range(0.5f, 2f);
                goingUp = Random.Range(0, 2) == 1;
            }

            moveVec *= data.speed * Time.deltaTime;

            moveVec += (Vector2)transform.localPosition;

            CheckDepth(moveVec, spawner);
            
            transform.localPosition = moveVec;
        }

        void CheckDepth(Vector2 pos, FishSpawner spawner, float maxTolerance = 0)
        {
            Vector2 bound = spawner.GetHeightBounds(data.level);
            bound.x += maxTolerance;
            bound.y -= maxTolerance;

            if (pos.y > bound.x)
            {
                goingUp = false;
                continueDirection = Random.Range(0.5f, 2f);
            }
            else if (pos.y < bound.y)
            {
                goingUp = true;
                continueDirection = Random.Range(0.5f, 2f);
            }
        }

        public void Chase(FishSpawner spawner)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * data.speed);
            if (Vector2.Distance(transform.position /*+ (transform.rotation * new Vector3(0.65f,0,0))*/, target.position) < data.pickupRange)
            {
                spawner.GetCaught(this);
                return;
            }

            float newRot = transform.rotation.eulerAngles.z;

            Vector3 diff = target.position - transform.position;
            
            if(target.position.x > transform.position.x)
            {
                faceLeft = false;
                Turn(false);
            }
            else
            {
                faceLeft = true;
                Turn(false);
            }

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + (faceLeft ? 180 : 0);

            transform.rotation = Quaternion.Euler(0, 0, rot_z);
        }
    }
}