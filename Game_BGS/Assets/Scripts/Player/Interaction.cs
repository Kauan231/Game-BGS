using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;

namespace Player {
    public class Interaction : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col) {
            GameObject objCollided = col.gameObject;
            if(objCollided.tag == "Interactable") {
                objCollided.GetComponent<IIteractable>().ShowInUI();
            }
            if((objCollided.tag == "Collectable")) {
                gameObject.GetComponent<ICollector>().Collect(objCollided.GetComponent<PhysicalItem>().itemData);
                col.GetComponent<ICollectable>().Collect();
                GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().ActivateSfx("Collect");
            }
        }
        
        void OnTriggerStay2D(Collider2D col) {
            GameObject objCollided = col.gameObject;
            if((objCollided.tag == "Interactable") && Input.GetKey(KeyCode.E)) {
                objCollided.GetComponent<IIteractable>().Interact();
            }
            
        }
        
        void OnTriggerExit2D(Collider2D col) {
           GameObject objCollided = col.gameObject;
            if(objCollided.tag == "Interactable") {
                objCollided.GetComponent<IIteractable>().HideInUI();
            } 
        }
    }

}
