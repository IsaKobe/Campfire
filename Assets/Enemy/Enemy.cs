using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]


public class Enemy : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    private float Speed;

    public virtual void Die() 
    {
        Debug.Log(Name + " died");
        Destroy(this);
    }
}
