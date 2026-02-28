using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Enemy<T> : MonoBehaviour where T : EnemyData
{
    public T EnemyStats;
    private float currentSpeed;
    public float CurrentHealth;
    private int currentRotateFrame = 50;

    public bool IsPlayerInView = false;
    private Rigidbody2D rb;

    protected Vector2 Originpoint;
    protected float WanderRadius;


    protected Transform player;
    [SerializeField] LayerMask playerLayer;

    protected virtual void Awake()
    {
        Originpoint = transform.position;
        WanderRadius = 10f;

        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = EnemyStats.MaxHealth;
        currentSpeed = EnemyStats.Speed;
        currentRotateFrame = EnemyStats.RotateFrame;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindPlayer();
    }
    protected virtual void FixedUpdate()
    {
        
        if (IsPlayerInView == true)
        {
            ChasePlayer();
        }
        if(Vector2.Distance(transform.position,player.position) >= 0.7f) 
        {
            Move();
        }

    }

    public void Move() 
    {
        CheckPanic();
        //každej xtej frame = random rotace
        currentRotateFrame--;
        if (!IsPlayerInView)
        {
            //Debug.Log("IS WANDERING");
            if (currentRotateFrame == 0)
            {

                RandomRotation();
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
            if(Vector2.Distance(transform.position,Originpoint) >= WanderRadius*0.95) 
        {
            Border();
        }
            Vector2 movement = transform.position + (-transform.right * currentSpeed / 3);
        rb.MovePosition(movement);
        
    }
    //hráè je blížko

    private void RandomRotation() 
    {
        float RotationChange = Random.Range(0, 360);

        transform.rotation = Quaternion.Euler(0, 0, RotationChange);
        currentRotateFrame = EnemyStats.RotateFrame;
    }
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
        //Debug.Log("CHASING PLAYER");
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
                //Debug.Log(hit.collider.tag);
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
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsPlayerInView)
        {
        Border();
        }
    }
    protected virtual void Border() 
    {
        transform.right = -transform.right;
        transform.Rotate(0,0,Random.Range(-20,20));
    }
    public virtual void OnDeath() 
    {
    }
}
