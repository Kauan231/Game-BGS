using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            items.Find(x => x.item.Title == _itemUpdated.item.Title).amount -= _itemUpdated.amount;
            if(items.Find(x => x.item.Title == _itemUpdated.item.Title).amount < 0) {
                items.Find(x => x.item.Title == _itemUpdated.item.Title).amount = 0;
            }
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
    }

}
