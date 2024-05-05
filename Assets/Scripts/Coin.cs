using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    PlayerMagnet magnet;
    Player player;

    CircleCollider2D coinColl;
    float speed = 0.1f;

    void Awake()
    {
        magnet = GameObject.Find("Player").GetComponent<PlayerMagnet>();
        player = GameObject.Find("Player").GetComponent<Player>();
        coinColl = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        if (magnet.Magnet)
        {
            speed = magnet.MagnetSpeed * Time.fixedDeltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < 5.5f)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed);
            }
        }
        else
        {
            coinColl.radius = 0.16f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Despawn")
        {
            Destroy(gameObject);
            Debug.Log("Coin Despawned");
        }
    }
}
