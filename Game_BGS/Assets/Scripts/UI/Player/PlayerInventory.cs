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
    [SerializeField] private TextMeshProUGUI ItemDescription, EquipBtnText, ItemTitle;
    [SerializeField] private Button EquipBtn;
    [SerializeField] private GameObject ShowMenu;
    [SerializeField] private Transform IconSpawn;
    
    private itemType SelectedType;
    [System.Serializable]
    public class InventoryType {
        public itemType type;
        public Button btnType;
        public Image Icon;
    }
    public List<InventoryType> typeSelectors = new List<InventoryType>();
    List<GameObject> Spawned = new List<GameObject>();

    private InventoryItem SelectedItem;

    public void Select(InventoryItem it) {
        SelectedItem = it;

        if(it.Equipped) {
            EquipBtnText.text = "Unequip";
        }
        else {
            EquipBtnText.text = "Equip";
        }

        ItemDescription.text = it.item.Description;
        ItemPreview.sprite =  it.item.Icon;
        ItemTitle.text = it.item.Title;
    }

    void Equip() {
        GameObject.FindWithTag("Player").GetComponent<EquipmentManager>().Equip(SelectedItem);
        GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Equip");
        SelectedItem = null;
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

    void Start() {
        EquipBtn.onClick.AddListener(Equip);
        foreach(InventoryType selector in typeSelectors) {
            selector.btnType.onClick.AddListener(delegate {ChangeType(selector.type);} );
        }
    }
    
    void ClearAll() {
        SelectedItem = null;
        Spawned = new List<GameObject>();
        ChangeType(itemType.Gem);
        ShowMenu.SetActive(false);
    }

    void StopAll() {
        Time.timeScale = 0f;
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Pause();
    }

    void StartAll() {
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<PlayerSound>().audio.Play();
    }

    void Spawn() {
        List<InventoryItem> toLoadInUI = GameObject.FindWithTag("Player").GetComponent<InventoryManager>().items; 
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
            Spawned.Add(created);
        }
    }

    void Show() {
        foreach(GameObject gm in Spawned) {
            if(gm) {
                if(gm.GetComponent<ItemDisplayInGrid>().data.item.type != SelectedType) {
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
        ShowMenu.SetActive(true);        
    }
    void OnDisable(){
        foreach(GameObject item in Spawned) {
            Destroy(item);
        }
        StartAll();
    }

    void Update() {
        Show();
        if(SelectedItem != null){
            ShowMenu.SetActive(true);
        } else {
            ShowMenu.SetActive(false);
        }
    }
}

