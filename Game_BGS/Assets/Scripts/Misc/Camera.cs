using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Transform player;

    void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Start() {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
