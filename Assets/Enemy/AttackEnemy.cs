using UnityEngine;

[CreateAssetMenu(fileName = "AttackEnemy", menuName = "Scriptable Objects/AttackEnemy")]
public class AttackEnemy : Enemy
{
    private float AttackDmg;
    private float AttackSpeed;
    private float Range;
    public virtual void Attack()
    {
        Debug.Log("Attacked with" + AttackDmg);
    }
}
