using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mechanics {
    public class InventoryManager : MonoBehaviour, ICollector
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public void AddItem(InventoryItem itemToAdd) {
            items.Add(itemToAdd);
        }
        public void RemoveItem(InventoryItem itemToRemove) {
            items.Remove(itemToRemove);
        }
        public void AddAmount(InventoryItem _itemUpdated) {
            items.Find(x => x.item.Title == _itemUpdated.item.Title).amount += _itemUpdated.amount;
        }
        public void SubtractAmount(InventoryItem _itemUpdated) {
            InventoryItem found = items.Find(x => x.item.Title == _itemUpdated.item.Title);
            int newAmount = found.amount - _itemUpdated.amount;
            items.Single(x => x == found).amount = newAmount;
        }

        public void Collect(InventoryItem _item) {
            InventoryItem item = items.Find(x => x.item.Title == _item.item.Title);
            if(item == null) {
                AddItem(_item);
            }
            else {
                AddAmount(_item);
            }
        }

        void Update() {
            foreach(InventoryItem i in items) {
                if(i.amount <= 0 ){
                    items.Remove(i);
                    break;
                }
            }
        }
    }

}
