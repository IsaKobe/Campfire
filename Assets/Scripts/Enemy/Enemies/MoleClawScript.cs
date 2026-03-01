using UnityEngine;

public class MoleClawScript : MonoBehaviour 
{
    [SerializeField] BossEnemy boss;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        boss.Attack();
    }
}
