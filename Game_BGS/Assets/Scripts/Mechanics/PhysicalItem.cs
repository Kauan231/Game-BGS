using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public class PhysicalItem : MonoBehaviour, ICollectable
    {
        public InventoryItem itemData;
        public void Collect() {
            Destroy(gameObject);
        }
    }
}

