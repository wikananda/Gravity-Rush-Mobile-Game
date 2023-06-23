using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    Player player;
    public float speed = 1f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = player.speed / 7;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, transform.position.z);
        }
    }
}
