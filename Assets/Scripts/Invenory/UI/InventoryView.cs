using NUnit;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts.Invenory.UI
{

    public partial class InventoryView : MonoBehaviour
    {
        UIDocument document;
        InventoryData inv;
        [SerializeField] InputAction inventoryToggle;
        VisualElement equip;
        VisualElement inventory;

        bool opened;
        void Close()
        {
            opened = false;
            document.rootVisualElement.style.display = DisplayStyle.None;
            transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        [ContextMenu("Open")]
        void Open()
        {
            opened = true;
            document.rootVisualElement.style.display = DisplayStyle.Flex;
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        void Awake()
        {
            inv = Inventory.Inv;
            document = GetComponent<UIDocument>();
            inventory = document.rootVisualElement.Q("Inventory");
            document.rootVisualElement.Q<Button>("Close").clicked += () => Close();

            inventoryToggle.performed += (e) => 
            {
                if (opened)
                    Close();
                else
                    Open();
            };

            equip = inventory[0];
            inventory = inventory[1];

            foreach (ItemData item in inv.items) {
                CreateItemDragable(item, inventory);
            }

            equip.Q("Helm").style.backgroundImage = new(inv.helm?.icon);
            equip.Q("Weapon").style.backgroundImage = new(inv.weapon?.icon);
            Close();
        }

        private void OnEnable()
        {
            inventoryToggle.Enable();
        }

        private void OnDisable()
        {
            inventoryToggle.Disable();
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
                /*case "Grenade":
                    break;*/
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
                    inv.weapon = weapon;
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
