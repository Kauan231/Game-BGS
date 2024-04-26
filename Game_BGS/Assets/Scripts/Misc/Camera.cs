using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool getSavedPosition;

    void Awake() {
        if(getSavedPosition) {
            Vector3 SpawnpointFound = GameObject.Find(PlayerPrefs.GetString("Spawnpoint", "DefaultSpawn")).transform.position;
            transform.position = new Vector3(SpawnpointFound.x, SpawnpointFound.y, transform.position.z);
        }
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
