using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "BaitEnemy", menuName = "Scriptable Objects/BaitEnemy")]
public class BaitEnemy : Enemy
{
    public virtual void Run() 
    {
    }
    public override void Die() 
    {
        Debug.Log("Drop Bait item");
    }
}
