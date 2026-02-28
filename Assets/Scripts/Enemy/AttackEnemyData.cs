using UnityEngine;

[CreateAssetMenu(fileName = "AttackEnemy", menuName = "Scriptable Objects/AttackEnemy")]
public class AttackEnemyData: EnemyData
{
    public float AttackDmg;
    public float AttackSpeed;
    public virtual void Attack()
    {
        Debug.Log("Attacked with" + AttackDmg);
    }
}
