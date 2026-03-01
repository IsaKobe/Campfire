using Framework.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "BaitEnemy", menuName = "Scriptable Objects/BaitEnemy")]
public class BaitEnemyData : EnemyData
{
    public BaitData bait;

    public BaitEnemyData() 
    {
        Range = 360f;
    }
    public virtual void Run() 
    {
    }
    public override void Die()
    {
        Debug.Log("Drop Bait item");
        // End level and add item to inventory more likely
    }
}
