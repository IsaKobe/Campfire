using Fish;
using Framework.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HookControls : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;


    [Header("Pull")]
    [SerializeField] float pullCooldown;
    [SerializeField] float currentPullCooldown;
    [SerializeField] float pullStrength;
    [SerializeField] float moveStrength;
    [SerializeField] float fallForce;

    [Header("Bait")]
    [SerializeField] float folorMargin = 2.5f;
    [SerializeField] BaitData baitData;
    [SerializeField] Transform cam;
    [SerializeField] FishSpawner spawner;
    Rigidbody2D rb;

    InputActionMap hookActions;
    InputAction hookMovement;
    InputAction hookStop;

    Fish.Fish fish;
    public int BaitLevel => baitData.rarity; 

    private void Awake()
    {
        fish = null;
        hookActions = actions.FindActionMap("Hook");
        hookMovement = hookActions.actions[0];
        hookActions.actions[1].performed += PullHook;
        hookStop = hookActions.actions[2];
        rb = GetComponent<Rigidbody2D>();

        SpriteRenderer renderer = rb.GetComponent<SpriteRenderer>();
        renderer.sprite = baitData.sprite;

        spawner.Init(baitData, this);
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
        if(fish == null)
        {
            if (currentPullCooldown > 0)
            {
                currentPullCooldown -= Time.deltaTime;
                if (currentPullCooldown < 0)
                    rb.gravityScale = Mathf.Cos(currentPullCooldown / pullCooldown) / fallForce;
                else
                    rb.gravityScale = 0.1f;
            }
            else
            {
                if (hookStop.WasPressedThisFrame())
                {
                    rb.linearVelocity = new();
                    rb.gravityScale = 0;
                }
                else if (hookStop.WasReleasedThisFrame())
                {
                    if (rb.gravityScale == 0)
                        rb.gravityScale = 0.1f;
                }
            }
        }
        else
        {
            transform.position = Vector2.Lerp(rb.position, transform.parent.position, Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        float diff = Mathf.Abs(cam.transform.position.y - rb.position.y);

        if (diff > folorMargin)
        {
            cam.transform.position = new(
                cam.transform.position.x, 
                Mathf.Lerp(cam.position.y, rb.position.y, Time.deltaTime),
                -10);
        }
    }

    void StopHook()
    {
        
    }

    void PullHook(InputAction.CallbackContext callbackContext)
    {
        if (currentPullCooldown > 0)
            return;
        currentPullCooldown = pullCooldown;
        rb.linearVelocityX = 0;
        rb.AddForce(new Vector2(hookMovement.ReadValue<float>() * moveStrength, pullStrength), ForceMode2D.Impulse);
    }

    public void PullFish(Fish.Fish _fish)
    {
        hookActions.Disable();
        fish = _fish;
        fish.transform.parent = transform;
        fish.transform.rotation = Quaternion.Euler(0, 0, -90);
        fish.transform.localPosition = new(0, -0.65f, 0);
        fish.GetComponent<CapsuleCollider2D>().enabled = false;
        fish.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        fish.GetComponent<SpriteRenderer>().flipX = false;

        rb.gravityScale = 0;
        rb.linearVelocity = new();
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
