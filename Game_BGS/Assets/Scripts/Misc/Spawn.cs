using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    private GameObject player;
    void Awake(){
        player = GameObject.FindWithTag("Player");
        if(SceneManager.GetActiveScene().name == "Main" && SpawnManager.PreviousScene != SceneManager.GetActiveScene().name) {
            player.transform.position = SpawnManager.ExitPoint;
        }
        else {
            player.transform.position = transform.position;
        }
    }
}
