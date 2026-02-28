using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Enemy<T> : MonoBehaviour where T : EnemyData
{
    public float CurrentHealth;
    public bool IsPlayerInView = false;
    private float currentSpeed;
    public T EnemyStats;

    private Rigidbody2D rb;
    private int currentRotateFrame = 50;

    private Transform player;
    [SerializeField] LayerMask playerLayer;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log(player.name);
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = EnemyStats.MaxHealth;
        currentSpeed = EnemyStats.Speed;
        currentRotateFrame = EnemyStats.RotateFrame;
    }

    // Update is called once per frame
    private void Update()
    {
        FindPlayer();
    }
    void FixedUpdate()
    {
        
        if (IsPlayerInView == true)
        {
            ChasePlayer();
        }
        Move();

    }

    public void Move() 
    {
        CheckPanic();
        //každej xtej frame = random rotace
        currentRotateFrame--;
        //float RotationChange = Random.Range((transform.rotation.z - 40),(transform.rotation.z+40));
        if (!IsPlayerInView)
        {
            Debug.Log("IS WANDERING");
            if (currentRotateFrame == 0)
            {
                
                float RotationChange = Random.Range(0, 360);

                transform.rotation = Quaternion.Euler(0, 0, RotationChange);
                currentRotateFrame = EnemyStats.RotateFrame;
            }
        }
        else 
        {
            if(currentRotateFrame == 0) 
            {
                currentRotateFrame = EnemyStats.RotateFrame;
            }
        }

            //každje fixed frame posune
            Vector2 movement = transform.position + (-transform.right * currentSpeed / 3);
        rb.MovePosition(movement);
        
    }
    //hráè je blížko
    public void CheckPanic()
    {
        if (IsPlayerInView)
        {
            currentSpeed = 1.5f * EnemyStats.Speed;
        }
        else
        {
            currentSpeed = EnemyStats.Speed;
        }

    }
    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            EnemyStats.Die();
            Destroy(gameObject);
        }
    }


    private void ChasePlayer()
    {
        Debug.Log("CHASING PLAYER");
        Vector3 direction = (player.position - transform.position).normalized;
        //Quaternion AngleDelta = Quaternion.FromToRotation(-transform.right, player.position);
        transform.right = -direction;
        //rb.MoveRotation(AngleDelta * transform.rotation);
        //rb.MovePosition(transform.position + (direction*));
    }

    private void FindPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= EnemyStats.Range)
        {
            //Debug.Log(Vector2.Distance(transform.position, player.position) + " " +EnemyStats.name);
            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            if (Vector2.Angle(-transform.right, directionToPlayer) < EnemyStats.ViewAngle / 2f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, EnemyStats.Range);

                if (hit.collider.tag == "Player")
                {
                Debug.Log(hit.collider.tag);
                    IsPlayerInView = true;
                }

            }
        }
        else
        {
            IsPlayerInView = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (EnemyStats == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyStats.Range);

        // Calculate the FOV lines based on transform.right (change to transform.up if your sprite faces up)
        Vector3 viewAngleStep1 = Quaternion.Euler(0, 0, EnemyStats.ViewAngle / 2) * -transform.right;
        Vector3 viewAngleStep2 = Quaternion.Euler(0, 0, -EnemyStats.ViewAngle / 2) * -transform.right;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleStep1 * EnemyStats.Range);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleStep2 * EnemyStats.Range);

        if (IsPlayerInView)
        {
            Gizmos.color = Color.green;
            if (player != null)
            {
                Gizmos.DrawLine(transform.position, player.position);
            }
        }
    }
}
