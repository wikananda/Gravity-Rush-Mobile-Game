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

     private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "MissileDespawn")
        {
            Destroy(gameObject);
            Debug.Log("Missile Despawned");
        }
    }
}
