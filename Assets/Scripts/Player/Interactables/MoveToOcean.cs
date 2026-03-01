using Assets.Scripts.Invenory.UI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player.Interactables
{
    public class MoveToOcean : InteractableTile
    {
        public override void Interact()
        {
            if (Inventory.Inv.baits.Count == 0)
                return;

            //open UI and chose a bait
            FindAnyObjectByType<WindowsToggles>().OpenBaitSelection();
        }
    }
}
