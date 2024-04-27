using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Mechanics;
using Player;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject ItemsDisplayPrefab, ItemsDisplayPrefabSkin;

    [SerializeField] private Image ItemPreview;
    [SerializeField] private TextMeshProUGUI ItemDescription, EquipBtnText;
    [SerializeField] private Button EquipBtn;
    [SerializeField] private GameObject ShowMenu;
    [SerializeField] private Transform IconSpawn;
    private InventoryItem ItemToEquip;
    private GameObject selectedItemInUi;
    private itemType SelectedType;

    [System.Serializable]
    public class InventoryType {
        public itemType type;
        public Button btnType;
        public Image Icon;
    }

    public List<InventoryType> typeSelectors = new List<InventoryType>();

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

    private void OnSelection(InventoryItem it) {
        if(it.Equipped) {
            EquipBtnText.text = "Unequip"; 
        }
        else {
            EquipBtnText.text = "Equip";
        }   
        ItemToEquip = it;
        ItemDescription.text = it.item.Description;
        ItemPreview.sprite =  it.item.Icon;
        ShowMenu.SetActive(true);
    }

    private void BtnAction() {
        GameObject.FindWithTag("Player").GetComponent<EquipmentManager>().Equip(ItemToEquip);
        GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Equip");
        ItemToEquip.Equipped = !ItemToEquip.Equipped;
        if(ItemToEquip.Equipped) {
            EquipBtnText.text = "Unequip"; 
        }
        else {
            EquipBtnText.text = "Equip";
        }
        UpdateUI();
    }

    private void Start() {
        UpdateUI();
        ChangeType(itemType.Gem);
        EquipBtn.onClick.AddListener(BtnAction);
    }

    private void UpdateUI() {
        GameObject[] UiItems = GameObject.FindGameObjectsWithTag("InventoryItem");

        if(UiItems.Length > 0) {
            for(int i=0; i < UiItems.Length; i++){
                Destroy(UiItems[i]);
            }
        }

        List<InventoryItem> toLoadInUI = GameObject.FindWithTag("Player").GetComponent<InventoryManager>().items; 

        foreach(InventoryType selector in typeSelectors) {
            selector.btnType.onClick.AddListener(delegate {ChangeType(selector.type);} );
        }

        foreach(InventoryItem it in toLoadInUI) {
            if(it.item.type != SelectedType) continue;

            GameObject toInstantiate;

            if(SelectedType == itemType.Clothes) {
                toInstantiate = ItemsDisplayPrefabSkin;
            }
            else {
                toInstantiate = ItemsDisplayPrefab;
            }

            GameObject created = Instantiate(toInstantiate);
            created.SetActive(true);
            created.gameObject.tag = "InventoryItem";
            created.transform.parent = IconSpawn;
            created.transform.position = ItemsDisplayPrefab.transform.position;
            created.GetComponent<ItemDisplayInGrid>().AmountText.text = it.amount.ToString();
            created.GetComponent<ItemDisplayInGrid>().ItemIcon.sprite = it.item.Icon;
            created.GetComponent<ItemDisplayInGrid>().Action.onClick.AddListener(delegate { OnSelection(it); });
            EquipmentManager equipManager = GameObject.FindWithTag("Player").GetComponent<EquipmentManager>();
            if(equipManager.EquippedItem != null && SelectedType != itemType.Clothes) {
                if(equipManager.EquippedItem.item == null) continue;
                if(it.item.Title == equipManager.EquippedItem.item.Title) {
                    created.GetComponent<ItemDisplayInGrid>().ItemIcon.color = new Color32(17,17,17,255); 
                    it.Equipped = true;
                }
            }  
            if(equipManager.EquippedSkin != null && SelectedType == itemType.Clothes) {
                if(equipManager.EquippedSkin.item == null) continue;
                if(it.item.Title == equipManager.EquippedSkin.item.Title) {
                    created.GetComponent<ItemDisplayInGrid>().ItemIcon.color = new Color32(17,17,17,255); 
                    it.Equipped = true;
                }
            }          
        }
    }

    private void OnEnable(){
        Time.timeScale = 0f;
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Pause();
        UpdateUI();
        ChangeType(itemType.Gem);
        ShowMenu.SetActive(false);
        ItemToEquip = null;
        ItemDescription.text = "";
        ItemPreview.sprite = null;
    }
    private void OnDisable() {
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Play();
    }
}

