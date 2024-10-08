using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameManager gameManager;
    float speed = 1f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gameManager.Speed / 7;
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
