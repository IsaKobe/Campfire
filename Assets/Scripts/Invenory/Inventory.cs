using Assets.Scripts.Invenory;
using Framework.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    public HelmData helm;
    public WeaponData weapon;
    public GrenadeData grenade;
    public List<ItemData> items;
    public List<BaitData> baits;
    public List<ItemData> hats;
}
public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryData data;
    public bool FirstVisit;
    static Inventory instance;
    public static BaitData selectedBait;
    public static BaitData SelectedBait => selectedBait;

    public static InventoryData Inv => instance.data;
    public static bool IsFirts { get => instance.FirstVisit; set => instance.FirstVisit = value; }

    public void Init(InventoryData _data)
    {
        FirstVisit = true;
/*
#if UNITY_EDITOR
        FirstVisit = false;
#endif*/

        data = _data;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void ChangeWeapon(WeaponData weapon)
    {
        Inv.weapon = weapon;
        Sword.LoadFromData(weapon);
    }

    public static void SelectBait(int selectedIndex)
    {
        selectedBait = Inv.baits[selectedIndex];
        Inv.baits.RemoveAt(selectedIndex);
    }
}
