using Assets.Scripts.Invenory.UI;
using UnityEngine;

namespace Assets.Scripts.Enemy.Enemies
{
    public class BaitEnemy : Enemy<BaitEnemyData>
    {
        [SerializeField] GameObject canvas;
protected override void Awake()
        {
            base.Awake();
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        public override void OnDeath()
        {
            FindAnyObjectByType<WindowsToggles>().OpenMenu(EnemyStats.bait);
            Inventory.Inv.baits.Add(EnemyStats.bait);
        }
    }
}

