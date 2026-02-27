using System.Runtime.CompilerServices;
using UnityEngine;

public class wormController : MonoBehaviour
{
    public float CurrentHealth;
    //IsDanger == jestli je hráè v rangi tak double speed a zdrhej do pryè
    private bool IsDanger;
    private float currentSpeed;
    public BaitEnemy baitStats;
    private Rigidbody2D rb;
    private float currentRotateFrame = 50;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = baitStats.MaxHealth;
        currentSpeed = baitStats.Speed;
        IsDanger = false;
        currentRotateFrame = baitStats.RotateFrame;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    
    public void Move() 
    {
        CheckPanic();
        //každej xtej frame = random rotace
        currentRotateFrame--;
        //float RotationChange = Random.Range((transform.rotation.z - 40),(transform.rotation.z+40));
        float RotationChange = Random.Range(0, 360);

        if (currentRotateFrame == 0) 
        {
            transform.rotation = Quaternion.Euler(0, 0, RotationChange);
            currentRotateFrame = baitStats.RotateFrame;
        }
        //každje fixed frame posune
        Vector2 movement = transform.position + (-transform.right * currentSpeed/3);
        rb.MovePosition(movement);
        
    }
    //hráè je blížko
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
