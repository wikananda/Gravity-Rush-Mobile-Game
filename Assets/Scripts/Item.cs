using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 1f;
    float initialSpeed;
    float movSpeed;
    Rigidbody2D rigid;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        initialSpeed = gameManager.Speed;
        speed = gameManager.Speed / 2;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movSpeed = gameManager.Speed * speed / (5f * initialSpeed);
        rigid.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * movSpeed, 0.2f);
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
