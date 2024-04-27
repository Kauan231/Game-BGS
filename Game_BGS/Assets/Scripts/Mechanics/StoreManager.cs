using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Mechanics;

public class StoreManager : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject ItemsDisplayPrefab;

    [SerializeField] private Image ProductPreview;
    [SerializeField] private TextMeshProUGUI ProductDescription, Total, AmountText;
    [SerializeField] private Button BuyOrSell, AddAmount, SubAmount;
    [SerializeField] private bool isBuy, isSell;
    [SerializeField] private GameObject ShowMenu;
    [SerializeField] private Transform IconSpawn;
    private InventoryItem ItemToBuy;
    private GameObject selectedItemInUi;
    private float TotalToPay;
    private int AmountToBuy;
    private itemType SelectedType;

    [System.Serializable]
    public class InventoryType {
        public itemType type;
        public Button btnType;
        public Image Icon;
    }

    public List<InventoryType> typeSelectors = new List<InventoryType>();

    private void Add() {
        AmountToBuy++;
        if(AmountToBuy > ItemToBuy.amount) {
            AmountToBuy = ItemToBuy.amount;
        }
    }
    private void Sub() {
        AmountToBuy--;
        if(AmountToBuy < 0) {
            AmountToBuy = 0;
        }
    }

    private void ChangeType(itemType type) {
        foreach(InventoryType select in typeSelectors) {
            if(select.type != type) {
                select.Icon.color = new Color32(17,17,17,255);
            }
            else {
                select.Icon.color = new Color32(255,255,255,255);
            }
        }
        SelectedType = type;
        UpdateUI();
    }

    private void OnSelection(InventoryItem it, GameObject _selectedItemInUi) {
        ShowMenu.SetActive(true);
        ItemToBuy = it;
        selectedItemInUi = _selectedItemInUi;
        ProductDescription.text = it.item.Description;
        ProductPreview.sprite =  it.item.Icon;
    }

    private void BtnAction() {
        float playerCurrentMoney = GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney;
        if(isBuy) {
            if(AmountToBuy > ItemToBuy.amount) return;
            if(TotalToPay <= playerCurrentMoney && TotalToPay != 0) {
                GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney -= TotalToPay;
                GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Buy");
                GameObject.FindWithTag("Player").GetComponent<InventoryManager>().Collect(new InventoryItem(ItemToBuy.item, AmountToBuy));

                int newAmount = ItemToBuy.amount - AmountToBuy;
                if(newAmount <= 0) {
                    items.Remove(ItemToBuy);
                    Destroy(selectedItemInUi);
                    selectedItemInUi = null;
                }
                else {
                    items.Single(x => x == ItemToBuy).amount = newAmount;
                    selectedItemInUi.gameObject.GetComponent<ItemDisplayInGrid>().AmountText.text = newAmount.ToString();
                }
            }
        }
        if(isSell) {
            if(AmountToBuy > ItemToBuy.amount) return;
            GameObject.FindWithTag("Player").GetComponent<InventoryManager>().SubtractAmount(new InventoryItem(ItemToBuy.item, AmountToBuy));
            GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Buy");
            GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney += TotalToPay;
            int newAmount = ItemToBuy.amount - AmountToBuy;
            if(newAmount <= 0) {
                Destroy(selectedItemInUi);
            }
            else {
                selectedItemInUi.gameObject.GetComponent<ItemDisplayInGrid>().AmountText.text = newAmount.ToString();
            }
        }
    }

    private void Start() {
        UpdateUI();
        ChangeType(itemType.Gem);
        AddAmount.onClick.AddListener(Add);
        SubAmount.onClick.AddListener(Sub);
        BuyOrSell.onClick.AddListener(BtnAction);
    }

    private void UpdateUI() {
        GameObject[] UiItems = GameObject.FindGameObjectsWithTag("InventoryItem");

        if(UiItems.Length > 0) {
            for(int i=0; i < UiItems.Length; i++){
                Destroy(UiItems[i]);
            }
        }

        List<InventoryItem> toLoadInUI = new List<InventoryItem>();

        if(isSell) {
            toLoadInUI = GameObject.FindWithTag("Player").GetComponent<InventoryManager>().items;  
        }
        if(isBuy) {
            toLoadInUI = items;
        }

        foreach(InventoryItem it in toLoadInUI) {
            if(it.item.type != SelectedType) return;

            GameObject created = Instantiate(ItemsDisplayPrefab);
            created.SetActive(true);
            created.gameObject.tag = "InventoryItem";
            created.transform.parent = IconSpawn;
            created.transform.position = ItemsDisplayPrefab.transform.position;
            created.GetComponent<ItemDisplayInGrid>().AmountText.text = it.amount.ToString();
            created.GetComponent<ItemDisplayInGrid>().PriceText.text = it.item.Price.ToString();
            created.GetComponent<ItemDisplayInGrid>().ItemIcon.sprite = it.item.Icon;
            created.GetComponent<ItemDisplayInGrid>().Action.onClick.AddListener(delegate { OnSelection(it, created); });
        }
        foreach(InventoryType selector in typeSelectors) {
            selector.btnType.onClick.AddListener(delegate {ChangeType(selector.type);} );
        }
    }

    private void OnEnable(){
        Time.timeScale = 0f;
        UpdateUI();
        ChangeType(itemType.Gem);
        ShowMenu.SetActive(false);
        ItemToBuy = null;
        selectedItemInUi = null;
        ProductDescription.text = "";
        ProductPreview.sprite = null;
    }
    private void OnDisable() {
        Time.timeScale = 1f;
    }

    private void Update() {
        if(ItemToBuy != null) {
            AmountText.text = AmountToBuy.ToString();
            TotalToPay = ItemToBuy.item.Price*AmountToBuy;
            Total.text = TotalToPay.ToString();
        }
    }
}

