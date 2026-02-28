using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Grenade : MonoBehaviour
{
    [SerializeField] public float radius = 3f;
    [SerializeField] public float throwForce = 10f;
    [SerializeField] public float dmg = 50f;
    [SerializeField] public float explosionDelay = 1.5f;
    private Vector3 mousePoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        Vector3 mousePos = Mouse.current.position.value;

        // 2. Set the Z-axis to the distance from the camera to the world plane.
        // In 2D, the camera is usually at z = -10, so the distance to z = 0 is 10.
        mousePos.z = Camera.main.nearClipPlane;

        // 3. Convert to World Space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        // 4. Force Z to 0 for 2D logic
        worldPosition.z = 0f;
        mousePoss= worldPosition;
        Vector3 diff = worldPosition - transform.position;


        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
        rb.AddForce(transform.up * throwForce, ForceMode2D.Impulse);
        Invoke("Explode", explosionDelay);
    }
    public void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                IDamagableEntity enemy = nearbyObject.GetComponent<IDamagableEntity>();
                Debug.Log(enemy);
                if (enemy != null)
                {
                    enemy.TakeDamage(dmg);
                }
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, mousePoss);
    }
}
