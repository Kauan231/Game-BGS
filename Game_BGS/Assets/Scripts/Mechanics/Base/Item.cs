using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public enum itemType { 
        Food,
        Clothes
    }
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public Sprite Sprite;
        public string Title;
        public string Description;
        public itemType type;
        public float Price;
    }

}
