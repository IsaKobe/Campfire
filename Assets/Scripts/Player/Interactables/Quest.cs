using Assets.Scripts.Invenory.UI;
using Framework.Scripts;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Quest : InteractableTile
{
    [SerializeField] List<InteractableTile> tilesToEnable;
    [SerializeField] BaitData reward;
    [SerializeField, TextArea(3,10)] string content;
    public override void Interact()
    {
        FindAnyObjectByType<WindowsToggles>().OpenQuest(displayMsg, content);
        if (reward != null)
        {
            FirstGame firtsGame = FindAnyObjectByType<FirstGame>();
            if (firtsGame)
            {
                Destroy(firtsGame.gameObject);
                Inventory.Inv.baits.Add(reward);
            }
        }

        foreach (var item in tilesToEnable)
        {
            item.Show();
        }
    }
}
