using Settings.Sound;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    public static class InventoryInit
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            GameObject game = new GameObject();
            Inventory inventory = game.AddComponent<Inventory>();
            inventory.Init(GameObject.Instantiate(Resources.Load<DefaultInventory>("DefaultInv")).inventory);

            
            MusicPlayer play = game.AddComponent<MusicPlayer>();
            play.Select(0);
            
        }
    }
}
