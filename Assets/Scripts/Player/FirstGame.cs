using Assets.Scripts.Invenory.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirstGame : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<InteractableTile> startingOn;
    [SerializeField] List<InteractableTile> secondPhase;
    private void Awake()
    {
        foreach (var item in startingOn)
        {
            item.Show();
        }

        if (!Inventory.IsFirts)
        {
            foreach (var item in secondPhase)
            {
                item.Show();
            }
            Destroy(gameObject);
            return;
        }

        foreach (var item in secondPhase)
        {
            item.Hide();
        }

        Inventory.IsFirts = false;
        player.transform.position = transform.position;
        player.GetComponent<Light2D>().enabled = true;

        FindAnyObjectByType<WindowsToggles>().OpenQuest(
            "Story",
            "After I graduated in Computer Science, AI took my job, and I fell into utter poverty. The only chance to reverse my fate is to find a pirate's treasure. However, I’ve been searching for years now, and it has certainly left its mark on me. But recently, I discovered a promising lead that brought me all the way to this cave in Egypt");
    }
}
