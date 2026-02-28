using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]


public class EnemyData : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Speed;
    public float Range;
    public float ViewAngle;
    public int RotateFrame;

    public virtual void Die() 
    {
        Debug.Log(Name + " died");
    }
}
