using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;
    bool state = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            state = !state;
            Inventory.SetActive(state);
        }
    }
}
