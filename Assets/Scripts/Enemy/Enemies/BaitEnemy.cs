




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
            //playergo.transform.position = new Vector3(target.position.x, target.position.y, playergo.transform.position.z);
            //Camera.main.transform.position = new Vector3(targetCamera.position.x, targetCamera.position.y, Camera.main.transform.position.z);
            canvas.SetActive(true);
        }
    }
}

