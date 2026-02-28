using Assets.Scripts.Invenory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    public HelmData helm;
    public WeaponData weapon;
    public List<ItemData> items;
}
public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryData data;
    static Inventory instance;
    public static InventoryData Inv => instance.data;


    public void Init(InventoryData _data)
    {
        data = _data;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
