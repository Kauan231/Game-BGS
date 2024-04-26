using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public interface ICollector
    {
        public void Collect(InventoryItem _item);
    }

}
