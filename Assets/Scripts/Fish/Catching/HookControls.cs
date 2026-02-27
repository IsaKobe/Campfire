using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HookControls : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;
    [SerializeField] float pullCooldown;
    [SerializeField] float currentPullCooldown;
    [SerializeField] float pullStrength;
    [SerializeField] float moveStrength;
    [SerializeField] float fallForce;
    Rigidbody2D rb;

    InputActionMap hookActions;
    InputAction hookMovement;

    
    private void Awake()
    {
        hookActions = actions.FindActionMap("Hook");
        hookMovement = hookActions.actions[0];
        hookActions.actions[1].performed += PullHook;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        hookActions.Enable();
    }
    private void OnDisable()
    {
        hookActions.Disable();
    }

    private void Update()
    {
        if(currentPullCooldown > 0)
        {
            currentPullCooldown -= Time.deltaTime;
            rb.gravityScale = Mathf.Cos(currentPullCooldown / pullCooldown) / fallForce;
        }
        else
        {
            rb.gravityScale = 0.1f;
        }
    }

    void PullHook(InputAction.CallbackContext callbackContext)
    {
        if (currentPullCooldown > 0)
            return;
        currentPullCooldown = pullCooldown;
        rb.linearVelocityX = 0;
        rb.AddForce(new Vector2(hookMovement.ReadValue<float>() * moveStrength, pullStrength), ForceMode2D.Impulse);
    }
}
