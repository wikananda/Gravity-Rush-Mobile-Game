using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    float speed = 1f;
    private void FixedUpdate() 
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * speed, 0.2f);
    }

     private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Despawn")
        {
            Destroy(gameObject);
            Debug.Log("Missile Despawned");
        }
    }
}
