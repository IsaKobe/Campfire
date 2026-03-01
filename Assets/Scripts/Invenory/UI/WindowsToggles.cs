using Framework.Scripts;
using NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Assets.Scripts.Invenory.UI
{

    public partial class WindowsToggles : MonoBehaviour
    {
        [SerializeField] UIDocument inventoryDoc;
        [SerializeField] UIDocument baitDoc;
        [SerializeField] UIDocument menuDoc;
        InventoryData inv;
        [SerializeField] InputAction inventoryToggle;
        [SerializeField] InputAction menuToggle;

        VisualElement equip;
        VisualElement inventory;

        bool inventoryOpened;
        bool menuOpened;

        void CloseInventory()
        {
            inventoryOpened = false;
            inventoryDoc.rootVisualElement.style.display = DisplayStyle.None;
            if(baitDoc != null)
                baitDoc.rootVisualElement.style.display = DisplayStyle.None;
            transform.GetChild(0).GetComponent<TilemapRenderer>().enabled = false;
            Time.timeScale = 1;
        }

        void OpenInventory()
        {
            inventoryOpened = true;
            transform.GetChild(0).GetComponent<TilemapRenderer>().enabled = true;
            inventoryDoc.rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
        public void OpenBaitSelection()
        {
            inventoryOpened = true;
            transform.GetChild(0).GetComponent<TilemapRenderer>().enabled = true;
            baitDoc.rootVisualElement.style.display = DisplayStyle.Flex;

            ListView view = baitDoc.rootVisualElement.Q<ListView>();
            view.itemsSource = inv.baits.OrderBy(q => q.rarity).ToList();
            Time.timeScale = 0;
        }

        void CloseMenu()
        {
            menuOpened = false;
            menuDoc.rootVisualElement.style.display = DisplayStyle.None;
            menuDoc.rootVisualElement.Q<VisualElement>("BaitImage").style.backgroundImage = null;
            menuDoc.rootVisualElement.Q<VisualElement>("BaitImage").style.height = 0;
            menuDoc.rootVisualElement.Q<Label>("Title").text = "Game Paused";
            menuDoc.rootVisualElement.Q<Button>("Home").style.display = DisplayStyle.Flex;
            Time.timeScale = 1;
        }

        void OpenMenu()
        {
            menuOpened = true;
            menuDoc.rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }

        public void OpenMenu(BaitData data)
        {
            menuOpened = true;
            menuDoc.rootVisualElement.Q<VisualElement>("BaitImage").style.backgroundImage = new(data.sprite);
            menuDoc.rootVisualElement.Q<VisualElement>("BaitImage").style.height = 300;
            menuDoc.rootVisualElement.Q<Label>("Title").text = "Bait Captured";
            menuDoc.rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }

        public void OpenQuest(string title, string content)
        {
            menuOpened = true;
            menuDoc.rootVisualElement.Q<Label>("Title").text = title;
            menuDoc.rootVisualElement.Q<Label>("Content").text = content;
            menuDoc.rootVisualElement.Q<Button>("Home").style.display = DisplayStyle.None;
            menuDoc.rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }

        void Awake()
        {
            inv = Inventory.Inv;
            if(SceneManager.GetActiveScene().name != "Fishing")
            {
                inventory = inventoryDoc.rootVisualElement.Q("Inventory");
                inventoryDoc.rootVisualElement.Q<Button>("Close").clicked += () => CloseInventory();
                inventoryToggle.performed += (e) =>
                {
                    if (menuOpened)
                        return;
                    if (inventoryOpened)
                        CloseInventory();
                    else
                        OpenInventory();
                };

                equip = inventory[0];
                inventory = inventory[1];

                foreach (ItemData item in inv.items)
                {
                    CreateItemDragable(item, inventory);
                }

                equip.Q("Weapon").style.backgroundImage = new(inv.weapon?.icon);
                equip.Q("Helm").style.backgroundImage = new(inv.helm?.icon);
                equip.Q("Grenade").style.backgroundImage = new(inv.grenade?.icon);
            }
            CloseInventory();


            menuDoc.rootVisualElement.Q<Button>("Unpause").clicked += () => CloseMenu();
            menuDoc.rootVisualElement.Q<Button>("Home").clicked += () => SceneManager.LoadScene("Home");
            if (SceneManager.GetActiveScene().name != "Home")
            {
                menuToggle.performed += (e) =>
                {
                    if (inventoryOpened)
                        CloseInventory();
                    else if (menuOpened)
                        CloseMenu();
                    else
                        OpenMenu();
                };
            }
            else
            {
                ListView view = baitDoc.rootVisualElement.Q<ListView>();
                view.fixedItemHeight = 64;
                view.makeItem = () =>
                {
                    VisualElement element = new();
                    element.Add(new());
                    element.Add(new Label());
                    return element;
                };
                view.bindItem = (el, i) =>
                {
                    el.AddToClassList("bait-elem");

                    el[0].style.backgroundImage = new(inv.baits[i].sprite);
                    el[0].style.aspectRatio = 1;

                    ((Label)el[1]).text = $"fish level: {inv.baits[i].rarity}";
                };
                
                baitDoc.rootVisualElement.Q<Button>("Close").clicked += CloseInventory;
                Button button = baitDoc.rootVisualElement.Q<Button>("StartHunt");
                button.clicked += () =>
                {
                    if (view.selectedIndex < 0)
                        return;
                    Inventory.SelectBait(view.selectedIndex);

                    SceneManager.LoadScene("Fishing");
                };
            }


            CloseMenu();
            if(baitDoc != null)
                baitDoc.rootVisualElement.style.display = DisplayStyle.None;
        }


        private void OnEnable()
        {
            inventoryToggle.Enable();
            menuToggle.Enable();
        }

        private void OnDisable()
        {
            inventoryToggle.Disable();
            menuToggle.Disable();
        }


        void CreateItemDragable(ItemData item, VisualElement parent)
        {
            VisualElement element = new();
            element.name = item.name;
            element.userData = item;
            element.style.backgroundImage = new StyleBackground(item.icon);
            element.AddToClassList("item-frame");
            parent.Add(element);
            element.AddManipulator(new DragableElement(element, this));
        }


        public void ChangeNear(VisualElement target)
        {
            List<VisualElement> elems = equip.Query<VisualElement>(className: "item-frame").ToList();

            float bestDist = float.MaxValue;
            VisualElement bestEl = null;
            for (int i = 0; i < elems.Count; i++)
            {
                if (!elems[i].worldBound.Overlaps(target.worldBound))
                    continue;
                float dist = (elems[i].worldBound.center - target.worldBound.center).sqrMagnitude;
                if(dist < bestDist)
                {
                    bestEl = elems[i];
                    bestDist = dist;
                }
            }
            if (bestEl == null)
                return;

            switch (bestEl.name)
            {
                case "Helm":
                    HelmData helm = target.userData as HelmData;
                    if (helm == null)
                        return;

                    if (inv.helm != null)
                    {
                        CreateItemDragable(inv.helm, inventory);
                        inv.items.Add(inv.helm);
                        inv.helm = null;
                    }
                    inv.helm = helm;
                    break;
                case "Grenade":
                    GrenadeData grenade = target.userData as GrenadeData;
                    if (grenade == null)
                        return;

                    if (inv.grenade != null)
                    {
                        CreateItemDragable(inv.grenade, inventory);
                        inv.items.Add(inv.grenade);
                        inv.grenade = null;
                    }
                    inv.grenade = grenade;
                    break;
                case "Weapon":
                    WeaponData weapon = target.userData as WeaponData;
                    if (weapon == null)
                        return;

                    if (inv.weapon != null)
                    {
                        CreateItemDragable(inv.weapon, inventory);
                        inv.items.Add(inv.weapon);
                        inv.weapon = null;
                    }
                    Inventory.ChangeWeapon(weapon);
                    break;
                default:
                    return;
            }
            bestEl.style.backgroundImage = new(((ItemData)target.userData).icon);
            inv.items.Remove((ItemData)target.userData);
            inventory.Remove(target);
        }

    }
}
