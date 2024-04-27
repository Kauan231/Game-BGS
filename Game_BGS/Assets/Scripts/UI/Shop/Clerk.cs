using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mechanics;

namespace UI {
    public class Clerk : MonoBehaviour, IIteractable
    {
        [SerializeField] private TextMeshProUGUI PressToInteract;
        [SerializeField] private GameObject SelectDialog;
        public void ShowInUI() {
            PressToInteract.gameObject.SetActive(true);
        }
        public void HideInUI() {
            PressToInteract.gameObject.SetActive(false);
            SelectDialog.SetActive(false);
        }
        public void Interact() {
           SelectDialog.SetActive(true);
        }
    }
}
