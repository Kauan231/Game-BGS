using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;

namespace Player {
    public class EquipmentManager : MonoBehaviour
    {
        public InventoryItem EquippedSkin, EquippedItem;

        public void Equip(InventoryItem inventoryToEquip) {
            SkinManager skManager = GameObject.FindWithTag("SkinManager").GetComponent<SkinManager>();
            if(inventoryToEquip.item.type == itemType.Clothes) {
                if(inventoryToEquip.Equipped) {
                    skManager.ChangeSkin("Base");
                    EquippedSkin = null;
                }
                else {
                    skManager.ChangeSkin(inventoryToEquip.item.Title);
                    EquippedSkin = inventoryToEquip;
                }
            }
            else {
                if(inventoryToEquip.Equipped) {
                    EquippedItem = null;
                }
                else {
                    EquippedItem = inventoryToEquip;
                }
            }
        }
    }

}
