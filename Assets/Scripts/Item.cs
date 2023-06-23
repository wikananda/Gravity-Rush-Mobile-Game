using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 1f;
    float initialSpeed;
    float movSpeed;

    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        initialSpeed = player.speed;
        speed = player.speed / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movSpeed = player.speed * speed / (5f * initialSpeed);
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * movSpeed, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Despawn")
        {
            Destroy(gameObject);
            Debug.Log("Item Despawned");
        }
    }
}
