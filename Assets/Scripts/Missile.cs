using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Player player;
    float speed = 1f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = player.speed / 7;
    }

    private void FixedUpdate() 
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * speed, 0.2f);
    }

     private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Despawn")
        {
            Destroy(gameObject);
            Debug.Log("Missile Despawned");
        }
    }
}
