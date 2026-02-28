using System;
using UnityEngine;

namespace Framework.Scripts
{
    [Serializable]
    public class BaitData
    {
        public Sprite sprite;
        public string name;
        public string description;
        public int rarity;

        public BaitData GetBait()
        {
            return this;
        }

        public BaitData(Sprite sprite, string name, string description, int rarity)
        {
            this.sprite = sprite;
            this.name = name;
            this.description = description;
            this.rarity = rarity;
        }
    }
}