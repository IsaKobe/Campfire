using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Assets.Scripts.Enemy.Enemies
{
    public class AttackEnemy : Enemy<AttackEnemyData>
    {
        public AttackEnemyData AtkEnemyData;
        Movements PlayerBaseScript;
        private float AtkCooldown;
        protected override void Awake()
        {
            base.Awake();
            AtkCooldown = AtkEnemyData.AttackSpeed;
            PlayerBaseScript = player.GetComponent<Movements>();


        }
        protected override void Update()
        {
            base.Update();
            AtkCooldown -= Time.deltaTime;
            if (AtkCooldown <= 0&& IsPlayerInView) 
            {
                //Debug.Log(Vector2.Distance(player.position,transform.position));
                if (Vector2.Distance(player.position, transform.position) < AtkEnemyData.Range)
                {
                    Attack();
                }
            }
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
        }
        public void Attack() 
        {

            PlayerBaseScript.DealDmg(AtkEnemyData.AttackDmg);
            Debug.Log("Attacked Dr Vodka :" + PlayerBaseScript.health + " Hp left");
            AtkCooldown = AtkEnemyData.AttackSpeed;
        }
    }
}
