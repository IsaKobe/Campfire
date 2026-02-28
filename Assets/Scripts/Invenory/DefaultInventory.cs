using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Invenory
{
    [CreateAssetMenu(fileName ="DefaultInv", menuName = "items/defInventory")]
    public class DefaultInventory : ScriptableObject
    {
        public InventoryData inventory;
    }
}
