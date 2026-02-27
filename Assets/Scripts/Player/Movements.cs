using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{


    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody2D rb;
    protected InputAction Move;
    Vector3 newMove;

    [SerializeField] float speed = 1000;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Move = inputActions.actionMaps[1].FindAction("Move");
    }
    private void OnEnable()
    {
        Move.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
    }
    void Update()
    {

        newMove = Move.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (newMove * speed));
    }

}


