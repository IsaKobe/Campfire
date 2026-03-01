using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;
public class Movements : MonoBehaviour
{


    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Sword sword;
    [SerializeField] Grenade GrenadePrefab;
    [SerializeField] Transform SpawnPoint;

    protected InputAction Move;
    protected InputAction Attack;
    protected InputAction Interact;
    protected InputAction ThrowBomb;
    Vector3 newMove;
    float attack;
    bool hasAttacked;
    public float maxHealth = 100;
    public float health;
    [SerializeField] float speed = 1;
    public InteractableTile interactableObject;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        Move = inputActions.actionMaps[1].FindAction("Move");
        Attack = inputActions.actionMaps[1].FindAction("Attack");
        ThrowBomb = inputActions.actionMaps[1].FindAction("ThrowBomb");
        Interact = inputActions.actionMaps[1].FindAction("Interact");
        Interact.performed += Interact_performed;
        hasAttacked = false;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        interactableObject?.Interact();
    }

    private void OnEnable()
    {
        Move.Enable();
        Attack.Enable();
        ThrowBomb.Enable();
        Interact.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Attack.Disable();
        Interact.Disable();
        ThrowBomb.Disable();

    }
    void Update()
    {
        newMove = Move.ReadValue<Vector2>();
        attack = Attack.ReadValue<float>();
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
        if (ThrowBomb.WasPerformedThisFrame() && Inventory.Inv.grenade != null)
        {
            Grenade grenade = Instantiate(GrenadePrefab, transform.position, Quaternion.identity);
            grenade.InitFromData(Inventory.Inv.grenade);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (newMove * speed * Time.deltaTime) );
    }
    public void DealDmg(float dmg)
    {
        health -= dmg;
        //Debug.Log(health + " HP LEFT");
        if (health <= 0)
        {
            OnDeath();
            // Die();
        }
    }
    private void OnDeath() 
    {
        Debug.Log("You died");
        enabled = false;
        health = maxHealth;
        transform.position = SpawnPoint.position;
        enabled = true;
        //Destroy(gameObject);
    }
}


