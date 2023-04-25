using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIController uicontrol;

    Rigidbody2D rigid;

    int gravityDirection = 1;
    public float jumpForceGrounded = 10f;
    public float jumpForceAir = 20f;
    public float speed = 5f;
    public float maxSpeed = 15f;

    public bool isGrounded = true;

    public float distance = 0f;
    public int coins = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        uicontrol.CoinUp(coins);
    }

    void Update()
    {
        JumpGravity();
    }

    void FixedUpdate()
    {
        distance += speed * Time.fixedDeltaTime;
    }


    void JumpGravity()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            gravityDirection *= -1;
            rigid.gravityScale *= -1;
            
            if(isGrounded)
            {
                rigid.AddForce(Vector3.up * -jumpForceGrounded * gravityDirection, ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(Vector3.up * -jumpForceAir * gravityDirection, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins += 5;
            //Destroy(other.gameObject);
            uicontrol.CoinUp(coins);
        }
    }
}
