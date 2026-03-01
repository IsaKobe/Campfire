using Fish;
using Framework.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] Transform cam;
    [SerializeField] FishSpawner spawner;
    Rigidbody2D rb;

    InputActionMap hookActions;
    InputAction hookMovement;
    InputAction hookStop;
    InputAction hookRear;
    [SerializeField]Transform rod;

    BaitData baitData;

    bool rearIn;

    Fish.Fish fish;

    public int BaitLevel { get => baitData.rarity; }

    private void Awake()
    {
        //Cursor.visible = false;
        fish = null;
        hookActions = actions.FindActionMap("Hook");
        hookMovement = hookActions.actions[0];
        hookRear = hookActions.actions[3];
        rb = GetComponent<Rigidbody2D>();

        baitData = Inventory.SelectedBait;
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
            if (hookRear.WasPressedThisFrame())
            {
                rearIn = true;
                rb.gravityScale = 0;
                rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -float.MinValue, 0);
            }
            else if(hookRear.WasReleasedThisFrame())
            {
                rearIn = false;
                rb.gravityScale = 0.1f;
            }
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

        if (rearIn)
        {
            Vector3 vec = rod.position - transform.position;
            if (Mathf.Abs(vec.x) < 0.1f && Mathf.Abs(vec.y) < 0.1f)
            {
                rb.linearVelocity = new();
                if(fish.data.equipDrop != null)
                    Inventory.Inv.items.Add(fish.data.equipDrop);
                enabled = false;
                SceneManager.LoadScene("Home");
                return;
            }
            if(rod.position.y < transform.position.y)
            {
                return;
            }

            vec.Normalize();
            vec.x += hookMovement.ReadValue<float>(); 
            rb.AddForce(vec);
           
        }
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
        GetComponent<CircleCollider2D>().enabled = false;

        rearIn = true;
    }
}
