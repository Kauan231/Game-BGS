using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Mechanics;
using Player;

public class StoreManager : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject ItemsDisplayPrefab, ItemsDisplayPrefabSkin;

    [SerializeField] private Image ProductPreview;
    [SerializeField] private TextMeshProUGUI ProductDescription, Total, AmountText, ProductTitle;
    [SerializeField] private Button BuyOrSell, AddAmount, SubAmount;
    [SerializeField] private bool isBuy, isSell;
    [SerializeField] private GameObject ShowMenu;
    [SerializeField] private Transform IconSpawn;
    private InventoryItem SelectedItem;
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
    List<GameObject> Spawned = new List<GameObject>();
    List<InventoryItem> playerItems = new List<InventoryItem>();

    private void Add() {
        AmountToBuy++;
        if(AmountToBuy > SelectedItem.amount) {
            AmountToBuy = SelectedItem.amount;
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
    }

    public void Select(InventoryItem it) {
        SelectedItem = it;
        ProductDescription.text = it.item.Description;
        ProductPreview.sprite =  it.item.Icon;
        ProductTitle.text = it.item.Title;
    }

    private void BtnAction() {
        float playerCurrentMoney = GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney;
        if(SelectedItem == null) return;
        if(isBuy) {
            if(AmountToBuy > SelectedItem.amount) return;
            InventoryItem found = playerItems.Find(x => x.item.Title == SelectedItem.item.Title);
            if((found != null) && found.item.type == itemType.Clothes) return;
            if(TotalToPay <= playerCurrentMoney && TotalToPay != 0) {
                GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney -= TotalToPay;
                GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Buy");
                GameObject.FindWithTag("Player").GetComponent<InventoryManager>().Collect(new InventoryItem(SelectedItem.item, AmountToBuy));

                int newAmount = SelectedItem.amount - AmountToBuy;
                SelectedItem.amount = newAmount;
                AmountToBuy = 0;
                AmountText.text = "";
                TotalToPay = 0f;
                Total.text = "";
            }
        }
        if(isSell) {
            if(AmountToBuy > SelectedItem.amount || AmountToBuy == 0) return;
            if(SelectedItem.Equipped) return;
            GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Buy");
            GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney += TotalToPay;
            int newAmount =  SelectedItem.amount - AmountToBuy;
            SelectedItem.amount = newAmount;
            AmountToBuy = 0;
            AmountText.text = AmountToBuy.ToString();
            TotalToPay = 0f;
            Total.text = TotalToPay.ToString();
        }
    }

    private void Start() {
        ChangeType(itemType.Gem);
        foreach(InventoryType selector in typeSelectors) {
            selector.btnType.onClick.AddListener(delegate {ChangeType(selector.type);} );
        }
        AddAmount.onClick.AddListener(Add);
        SubAmount.onClick.AddListener(Sub);
        BuyOrSell.onClick.AddListener(BtnAction);
    }
    
    void Spawn() {
        List<InventoryItem> toLoadInUI = new List<InventoryItem>();
        playerItems = GameObject.FindWithTag("Player").GetComponent<InventoryManager>().items;  

        if(isSell) {
            toLoadInUI = playerItems;  
        }
        if(isBuy) {
            toLoadInUI = items;
        }

        foreach(InventoryItem it in toLoadInUI) {
            if(it == null) continue; 
        
            GameObject toInstantiate;

            if(it.item.type == itemType.Clothes) {
                toInstantiate = ItemsDisplayPrefabSkin;
            }
            else {
                toInstantiate = ItemsDisplayPrefab;
            }

            GameObject created = Instantiate(toInstantiate);
            if(it.item.type == SelectedType) {
                created.SetActive(true);
            };
            created.gameObject.tag = "InventoryItem";
            created.transform.parent = IconSpawn;
            created.transform.position = ItemsDisplayPrefab.transform.position;
            created.GetComponent<ItemDisplayInGrid>().AmountText.text = it.amount.ToString();
            created.GetComponent<ItemDisplayInGrid>().ItemIcon.sprite = it.item.Icon;
            created.GetComponent<ItemDisplayInGrid>().data = it;
            created.GetComponent<ItemDisplayInGrid>().isShop = true;
            Spawned.Add(created);
        }
    }

    void ClearAll() {
        SelectedItem = null;
        selectedItemInUi = null;
        ProductDescription.text = "";
        ProductTitle.text = "";
        ProductPreview.sprite = null;
        Spawned = new List<GameObject>();
        playerItems = new List<InventoryItem>();
        ChangeType(itemType.Gem);
        ShowMenu.SetActive(false);
    }

    void StopAll() {
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Pause();
        Time.timeScale = 0f;
        
    }

    void StartAll() {
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Play();   
    }

    void Show() {
        foreach(GameObject gm in Spawned) {
            if(gm) {
                if((gm.GetComponent<ItemDisplayInGrid>().data.item.type != SelectedType) || gm.GetComponent<ItemDisplayInGrid>().data.Equipped ) {
                    gm.SetActive(false);
                }
                else {
                    gm.SetActive(true);
                }
            } else {
                Spawned.Remove(gm);
                break;
            }
        }
    }

    void OnEnable() {
        ClearAll();
        StopAll();
        Spawn();
        ShowMenu.SetActive(false);        
    }
    void OnDisable(){
        foreach(GameObject item in Spawned) {
            Destroy(item);
        }
        StartAll();
    }

    private void Update() {
        Show();
        if(SelectedItem != null) {
            ShowMenu.SetActive(true);  
            AmountText.text = AmountToBuy.ToString();
            TotalToPay = SelectedItem.item.Price*AmountToBuy;
            Total.text = TotalToPay.ToString();
        } else {
            ShowMenu.SetActive(false);
        }
    }
}

