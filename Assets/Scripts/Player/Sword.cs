using Assets.Scripts.Invenory;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public WeaponData data;
    public float followSharpness = 0.1f;

    float cooldown;
    private float cooldownRemaining = 0;
    static Sword instance;
    private void Awake()
    {
        instance = this;
        LoadFromData(Inventory.Inv.weapon);
    }
    private void OnDestroy()
    {
        instance = null;
    }

    public static void LoadFromData(WeaponData _data)
    {
        if (instance == null)
            return;
        instance.Init(_data);
    }
    void Init(WeaponData _data)
    {
        if (_data == null)
        {
            _data = ScriptableObject.CreateInstance<WeaponData>();
            _data.damage = 0;
            _data.name = "empty";
            _data.cooldown = 0;

            data = _data;
            cooldown = _data.cooldown;

            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            data = _data;

            float _cooldown = _data.cooldown;
            _cooldown *= 1 / Time.fixedDeltaTime;
            cooldown = Mathf.Round(_cooldown);


            GetComponent<SpriteRenderer>().sprite = _data.icon;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        cooldownRemaining = 0;
        transform.localPosition = new(0, -0.9f);
        transform.localRotation = Quaternion.Euler(0,0,90);
    }
    
    private void FixedUpdate()
    {
        if (data.name == "empty")
            return;

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
        if (cooldownRemaining > 0 || data.name == "empty")
        {
            return;
        }
        cooldownRemaining = cooldown;
        Debug.Log("Attacking");
    }
}
