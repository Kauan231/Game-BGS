using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    void Awake(){
        if(SceneManager.GetActiveScene().name == "Main" && SpawnManager.PreviousScene != "Main") {
            transform.position = SpawnManager.ExitPoint;
        }
        else {
            transform.position = GameObject.FindWithTag("Entrance").transform.position;
        }
        
    }
}
