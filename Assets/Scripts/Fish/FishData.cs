using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fish
{
    [CreateAssetMenu(fileName = "fish", menuName = "fishData")]
    public class FishData : ScriptableObject
    {
        public int level = 1;
        public float speed = 1;
        //public Equip equip;
        public Sprite sprite;
        public float seeRange = 1;
    }
}
