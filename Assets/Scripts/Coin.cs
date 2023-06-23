using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Player player;
    float speed = 0.1f;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (player.coinMagnet)
        {
            speed = player.coinMagnetSpeed * Time.fixedDeltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < 5.5f)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed);
            }
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
