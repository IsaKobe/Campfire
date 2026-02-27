using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Fish
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] List<FishData> fishes;
        [SerializeField] float width;
        [SerializeField] float lineHeight;
        [SerializeField] float linePadding;
        //[SerializeField] Vector2 max;
        [SerializeField] float verticalMove;
        [SerializeField] int fishCount = 20;

        [SerializeField] int level = 5;


        List<Fish> activeFish = new();

        private void OnDrawGizmos()
        {
            for (int i = 0; i < level; i++)
            {
                Vector2 vector2 = GetHeightBounds(i);
                Gizmos.DrawLine(new(-width, vector2.x), new(width, vector2.x));
                Gizmos.DrawLine(new(-width, vector2.y), new(width, vector2.y));
            }
        }

        private void Start()
        {
            
            CreateBoundsVertical(-(width + 0.5f));
            CreateBoundsVertical(width + 0.5f);

            CreateBoundsHorizontal(0.5f);
            CreateBoundsHorizontal(GetHeightBounds(level - 1).y - 0.5f);

            for (int i = 0; i < fishCount; i++)
            {
                CreateFish(fishes[Random.Range(0, fishes.Count)]);
            }
        }

        void CreateBoundsVertical(float x)
        {
            Vector2 bounds = GetHeightBounds(level - 1);

            GameObject gameObject = new GameObject("bound");
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;            

            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            gameObject.transform.position = new Vector2(x, bounds.y / 2);
            collider.size = new Vector2(0.5f, -bounds.y);
        }

        void CreateBoundsHorizontal(float y)
        {
            Vector2 bounds = GetHeightBounds(level - 1);

            GameObject gameObject = new GameObject("bound");
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;

            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            gameObject.transform.position = new Vector2(0, y);
            collider.size = new Vector2(width*2, 0.5f);
        }


        public void CreateFish(FishData data)
        {
            GameObject gameObject = new();
            gameObject.transform.parent = transform;
            Fish f = gameObject.AddComponent<Fish>();

            Vector2 heightBounds = GetHeightBounds(data.level);
            Vector2 spawnPos = new(Random.Range(-width, width), Random.Range(heightBounds.x, heightBounds.y));
            f.Init(data, spawnPos, true);
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = data.sprite;

            activeFish.Add(f);
        }

        public void Update()
        {
            foreach (Fish f in activeFish)
            {
                MoveFish(f);
            }
        }

        Vector2 GetHeightBounds(int level)
        {
            float min = -(level * lineHeight + level * linePadding);
            float max = min - lineHeight;
            return new Vector2(min, max);
        }

        void MoveFish(Fish fish)
        {
            Vector2 moveVec = fish.faceLeft ? Vector2.left : Vector2.right;
            if (fish.continueDirection > 0)
            {
                moveVec.y = (fish.goingUp ? 0.2f : -0.2f);
                fish.continueDirection -= Time.deltaTime;
            }
            else
            {
                fish.continueDirection = Random.Range(0.5f, 2f);
                fish.goingUp = Random.Range(0, 2) == 1;
            }

            moveVec *= fish.data.speed * Time.deltaTime;

            moveVec += (Vector2)fish.transform.localPosition;

            if (moveVec.x < -width || moveVec.x > width)
                fish.Turn();

            Vector2 bound = GetHeightBounds(fish.data.level);

            if (moveVec.y > bound.x)
            {
                fish.goingUp = false;
                fish.continueDirection = Random.Range(0.5f, 2f);
            }
            else if (moveVec.y < bound.y)
            {
                fish.goingUp = true;
                fish.continueDirection = Random.Range(0.5f, 2f);
            }
            fish.transform.localPosition = moveVec;
        }
    }
}
