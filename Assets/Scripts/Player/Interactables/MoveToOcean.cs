using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player.Interactables
{
    public class MoveToOcean : InteractableTile
    {
        public override void Interact()
        {
            SceneManager.LoadScene("Fishing");
        }
    }
}
