using Assets.Scripts.Invenory;
using Settings.Sound;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Grenade : MonoBehaviour
{
    GrenadeData data;
    private Vector3 mousePoss;
    public void Explode()
    {
        MusicPlayer.PlaySoundEffect(data.clip);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, data.radius);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                IDamagableEntity enemy = nearbyObject.GetComponent<IDamagableEntity>();
                Debug.Log(enemy);
                if (enemy != null)
                {
                    enemy.TakeDamage(data.dmg);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (data == null)
            return;
        Gizmos.DrawWireSphere(transform.position, data.radius);
        Gizmos.DrawLine(transform.position, mousePoss);
    }

    public void InitFromData(GrenadeData grenade)
    {
        data = grenade;

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
        mousePoss = worldPosition;
        Vector3 diff = worldPosition - transform.position;


        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
        rb.AddForce(transform.up * data.throwForce, ForceMode2D.Impulse);
        Invoke("Explode", data.explosionDelay);
    }
}
