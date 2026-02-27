using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fish
{
    [CreateAssetMenu(fileName = "fish", menuName = "fishData")]
    public class FishData : ScriptableObject
    {
        public int level;
        public float speed;
        //public Equip equip;
        public Sprite sprite;
    }
}
