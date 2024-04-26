using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Utilities;

namespace UI {
    public class Door : MonoBehaviour, IIteract
    {
        [SerializeField] private TextMeshProUGUI PressToInteract;
        [SerializeField] private string SceneToAccess;
        [SerializeField] private bool SavePosition;
        [SerializeField] private GameObject Spawnpoint;
        
        public void ShowInUI() {
            PressToInteract.gameObject.SetActive(true);
        }
        public void HideInUI() {
            PressToInteract.gameObject.SetActive(false);
        }
        public void Interact() {
            if(SavePosition) {
                PlayerPrefs.SetString("Spawnpoint", Spawnpoint.name);
            }
            SceneManager.LoadSceneAsync(SceneToAccess);
        }

        public void Update() {
            Debug.Log(PlayerPrefs.GetFloat("X"));
        }
    
    }
}
