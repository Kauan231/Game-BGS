using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance {get; private set;}
    public static Vector2 ExitPoint;
    public static string PreviousScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PreviousScene = SceneManager.GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
