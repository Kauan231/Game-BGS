using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mechanics;
using TMPro;

public class ItemDisplayInGrid : MonoBehaviour
{
    public TextMeshProUGUI AmountText, PriceText;
    public Image ItemIcon;
    public Button Action;
    public InventoryItem data;
    public bool isShop;

    void Start() {
        Action.onClick.AddListener(SelectItem);
    }

    void SelectItem() {   
        if(!isShop) {
            GameObject.FindWithTag("InventoryManager").GetComponent<PlayerInventory>().Select(data);
        }
        else {
            GameObject.FindWithTag("StoreManager").GetComponent<StoreManager>().Select(data);
        }
    }

    void Update() {
        if(data != null) {
            AmountText.text = data.amount.ToString();
            PriceText.text = data.item.Price.ToString();
            ItemIcon.sprite = data.item.Icon;
            if(data.Equipped) {
                ItemIcon.color = new Color32(17,17,17,255);
            }
            else {
                ItemIcon.color = new Color32(255,255,255,255);
            }
            if(data.amount <= 0) {
                Destroy(gameObject);
            }
        }
        
    }
}
