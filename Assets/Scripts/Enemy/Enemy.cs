using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]


public class Enemy : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Health;
    public float Speed;
    public float Range;
    public float damage;
    public virtual void Die() 
    {
        Debug.Log(Name + " died");
        Destroy(this);
    }

}