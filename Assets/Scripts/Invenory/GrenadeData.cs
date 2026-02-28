using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    public class GrenadeData : ItemData
    {
        public float radius = 3f;
        public float throwForce = 10f;
        public float dmg = 50f;
        public float explosionDelay = 1.5f;
    }
}
