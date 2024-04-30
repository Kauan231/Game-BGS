using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;

namespace Player {
    public class EquipmentManager : MonoBehaviour
    {
        public InventoryItem EquippedSkin, EquippedItem;

        void OnEnable() {
            List<InventoryItem> found = GameObject.FindWithTag("Player").GetComponent<InventoryManager>().items; 
            foreach(InventoryItem it in found) {
                if(it.Equipped && it.item.type == itemType.Clothes) {
                    EquippedSkin = it;
                }
            }
        }

        public void Equip(InventoryItem inventoryToEquip) {
            SkinManager skManager = GameObject.FindWithTag("SkinManager").GetComponent<SkinManager>();
            if(inventoryToEquip.item.type == itemType.Clothes) {
                if(inventoryToEquip == EquippedSkin) {
                    EquippedSkin.Equipped = false;
                    inventoryToEquip.Equipped = false;
                    EquippedSkin = null;
                    skManager.ChangeSkin("Base");
                }
                else {
                    skManager.ChangeSkin(inventoryToEquip.item.Title);
                    if(EquippedSkin != null) {
                        EquippedSkin.Equipped = false;
                    }
                    
                    inventoryToEquip.Equipped = true;
                    EquippedSkin = inventoryToEquip;
                }
            }
            else {
                if(inventoryToEquip == EquippedItem) {
                    if(EquippedItem != null) {
                        EquippedItem.Equipped = false;
                    }
                    
                    inventoryToEquip.Equipped = false;
                    EquippedItem = null;
                }
                else {
                    //EquippedItem.Equipped = false;
                    EquippedItem = inventoryToEquip;
                    inventoryToEquip.Equipped = true;
                } 
            }
        }
    }

}
