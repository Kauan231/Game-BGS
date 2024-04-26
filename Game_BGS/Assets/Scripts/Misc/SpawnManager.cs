using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance {get; private set;}
    public static Vector2 ExitPoint;
    public static string PreviousScene;

    public Vector2 exit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PreviousScene = SceneManager.GetActiveScene().name;
    }
    public void Update() {
        exit = ExitPoint;
    }
}
