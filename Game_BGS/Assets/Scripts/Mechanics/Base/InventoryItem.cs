using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    [System.Serializable]
    public class InventoryItem 
    {
        public Item item;
        public int amount;
        public bool Equipped;

        public InventoryItem(Item _item, int _amount) {
            item = _item;
            amount = _amount;
        }
    }
}