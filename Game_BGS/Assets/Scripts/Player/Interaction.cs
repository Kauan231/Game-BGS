using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Player {
    public class Interaction : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col) {
            GameObject objCollided = col.gameObject;
            if(objCollided.tag == "Interactable") {
                objCollided.GetComponent<IIteract>().ShowInUI();
            }
        }
        
        void OnTriggerStay2D(Collider2D col) {
            GameObject objCollided = col.gameObject;
            if((objCollided.tag == "Interactable") && Input.GetKey(KeyCode.E)) {
                objCollided.GetComponent<IIteract>().Interact();
            }
        }
        
        void OnTriggerExit2D(Collider2D col) {
           GameObject objCollided = col.gameObject;
            if(objCollided.tag == "Interactable") {
                objCollided.GetComponent<IIteract>().HideInUI();
            } 
        }
    }

}
