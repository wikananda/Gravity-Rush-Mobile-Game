using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 25f;
    Vector3 endPos;
    Vector3 playerPos;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        endPos = transform.position;
    }
}
