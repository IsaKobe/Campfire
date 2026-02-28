using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;
public class Movements : MonoBehaviour
{


    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Sword sword;
    [SerializeField] GameObject GrenadePrefab;
    protected InputAction Move;
    protected InputAction Attack;
    protected InputAction ThrowBomb;
    Vector3 newMove;
    float attack;
    bool hasAttacked;
    bool hasBombed;
    public float maxHealth = 100;
    public float health = 100;
    private float throwBomb;
    [SerializeField] float speed = 1;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Move = inputActions.actionMaps[1].FindAction("Move");
        Attack = inputActions.actionMaps[1].FindAction("Attack");
        ThrowBomb = inputActions.actionMaps[1].FindAction("ThrowBomb");
        hasAttacked = false;
        hasBombed = false;
    }
    private void OnEnable()
    {
        Move.Enable();
        Attack.Enable();
        ThrowBomb.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Attack.Disable();
        ThrowBomb.Disable();

    }
    void Update()
    {

        newMove = Move.ReadValue<Vector2>();
        attack = Attack.ReadValue<float>();
        throwBomb = ThrowBomb.ReadValue<float>();
        if (attack != 0 && !hasAttacked)
        {
            Debug.Log("Player attacking");
            hasAttacked = attack != 0;
            sword.Attack();
        }
        else if (attack == 0)
        {
            hasAttacked = false;
        }
        if (throwBomb != 0 && !hasBombed)
        {
            Debug.Log("Throw bomb!!");
            hasBombed = throwBomb != 0;
            Instantiate(GrenadePrefab, transform);
        }
        else if (throwBomb== 0)
        {
            hasBombed = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (newMove * speed * Time.deltaTime) );
    }
    public void DealDmg(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            // Die();
        }
    }
}


