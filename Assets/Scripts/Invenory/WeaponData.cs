using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    [CreateAssetMenu(fileName ="weapon", menuName ="Items/Weapon")]
    public class WeaponData : ItemData
    {
        public float damage;
        public float cooldown;
    }
}
