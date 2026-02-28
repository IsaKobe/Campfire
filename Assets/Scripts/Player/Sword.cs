using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float cooldown;
    public float followSharpness = 0.1f;

    private float cooldownRemaining = 0;
    private void Start()
    {
        cooldown *= 1 / Time.fixedDeltaTime;
        cooldown = Mathf.Round(cooldown);
    }
    private void FixedUpdate()
    {
        float percentDone = cooldownRemaining / cooldown;

        if (percentDone == 0)
        {
            return;
        }
        cooldownRemaining--;
        //Debug.Log(String.Format("percentdone: {0}, cooldown: {1}, remaining: {2}, angle: {3}", percentDone, cooldown, cooldownRemaining, 360 * percentDone));
        transform.RotateAround(transform.parent.position, Vector3.forward, 360 * (1 / cooldown) * -1);
    }

    public void Attack()
    {
        if (cooldownRemaining > 0)
        {
            return;
        }
        cooldownRemaining = cooldown;
        Debug.Log("Attacking");
    }
}
