using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Mechanics;

namespace UI {
    public class Door : MonoBehaviour, IIteractable
    {
        [SerializeField] private TextMeshProUGUI PressToInteract;
        [SerializeField] private string SceneToAccess;
        [SerializeField] private bool isEntrance;
        [SerializeField] private GameObject Entrance;
        
        public void ShowInUI() {
            PressToInteract.gameObject.SetActive(true);
        }
        public void HideInUI() {
            PressToInteract.gameObject.SetActive(false);
        }
        public void Interact() {
            if(isEntrance) {
                SpawnManager.ExitPoint = Entrance.transform.position;
            }
            SpawnManager.PreviousScene =  SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(SceneToAccess);
        }
    }
}
