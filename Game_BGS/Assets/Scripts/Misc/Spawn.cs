using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    private GameObject player;
    void OnEnable(){
        player = GameObject.FindWithTag("Player");
        if(SpawnManager.PreviousScene != SceneManager.GetActiveScene().name) {
            if(SceneManager.GetActiveScene().name == "Main") {
                player.transform.position = SpawnManager.ExitPoint;
            }
            else {
                player.transform.position = transform.position;
            }
        }
    }    
}
