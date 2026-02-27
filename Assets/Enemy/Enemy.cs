using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]


public class Enemy : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Speed;
    public float Range;
    public virtual void Die() 
    {
        Debug.Log(Name + " died");
        Destroy(this);
    }
}
