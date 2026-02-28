using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    [CreateAssetMenu(fileName ="item", menuName ="Items/item")]
    public class ItemData : ScriptableObject
    {
        public string name;
        public string description;
        public Sprite icon;
    }
}
