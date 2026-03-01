using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    [CreateAssetMenu(fileName = "grenade", menuName ="Items/GrenadeData")]
    public class GrenadeData : ItemData
    {
        public AudioClip clip;
        public float radius = 3f;
        public float throwForce = 10f;
        public float dmg = 50f;
        public float explosionDelay = 1.5f;
    }
}
