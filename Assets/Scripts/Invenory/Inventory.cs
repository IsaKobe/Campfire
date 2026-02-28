using Assets.Scripts.Invenory;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public HelmData helm;
    public WeaponData weapon;
    public List<ItemData> items;

    private void Awake()
    {
        
    }
}
