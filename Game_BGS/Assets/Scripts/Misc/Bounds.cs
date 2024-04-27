using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Vector2 max;
    public Vector2 min;
    private Transform Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Player.position = new Vector2(Mathf.Clamp(Player.position.x, min.x, max.x), Mathf.Clamp(Player.position.y, min.y, max.y));
    }
}
