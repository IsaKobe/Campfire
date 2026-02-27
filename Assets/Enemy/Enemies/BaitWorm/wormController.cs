using UnityEngine;

public class wormController : MonoBehaviour
{
    public float CurrentHealth;
    //IsDanger == jestli je hráè v rangi tak double speed a zdrhej do pryè
    private bool IsDanger;
    private float currentSpeed;
    public BaitEnemy baitStats;
    private Rigidbody2D rb;
    private float rotateFrame = 10;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = baitStats.MaxHealth;
        currentSpeed = baitStats.Speed;
        IsDanger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    
    public void Move() 
    {
        CheckPanic();
        rotateFrame--;
        
        if (rotateFrame ==0) 
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            if (transform.rotation.z > 90 && transform.rotation.z < 270)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            rotateFrame = 10;
        }
            
        Vector2 movement = transform.position + (-transform.right * currentSpeed);
        rb.MovePosition(movement);
        
    }
    public void CheckPanic()
    {
        if (IsDanger)
        {
            currentSpeed *= 2;
        }
        else
        {
            currentSpeed = baitStats.Speed;

        }

    }
    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            baitStats.Die();
            Destroy(gameObject);
        }
    }
}
