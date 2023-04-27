using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIController uicontrol;

    Rigidbody2D rigid;

    // ABILITY PROPERTIES
    int gravityDirection = 1;
    public float jumpForceGrounded = 10f;
    public float jumpForceAir = 20f;
    public float speed = 5f;
    public float maxSpeed = 15f;
    public float coinMagnetSpeed = 5f;

    // STATE PROPERTIES
    public bool isGrounded = true;
    public bool coinMagnet = false;
    public bool invincible = false;
    public bool flash = false; // Flash movement when gravity change

    // GAME PROPERTIES
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
            
            if (flash)
            {
                rigid.AddForce(Vector3.up * -jumpForceGrounded * 300 * gravityDirection, ForceMode2D.Impulse);
            }
            // A workaround for the floating feel problem when jumping
            else if(isGrounded)
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
            Destroy(other.gameObject);
            uicontrol.CoinUp(coins); // Call CoinUp method in UIController to update coin text
        }
        
        if (other.gameObject.tag == "Obstacle")
        {
            if (!invincible)
            {
                Debug.Log("Game Over");
            }
        }
    }
}
