using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIController uicontrol;
    [SerializeField] GameObject rocket;

    Rigidbody2D rigid;

    // ABILITY PROPERTIES ====================
    int gravityDirection = 1;
    public float jumpForceGrounded = 10f;
    public float jumpForceAir = 20f;
    public float speed = 5f;
    public float maxSpeed = 15f;
    public float coinMagnetSpeed = 5f;

    // STATE PROPERTIES =======================
    public bool isGrounded = true;
    public bool coinMagnet = false;
    public bool invincible = false;
    public bool flash = false; // Flash movement when gravity change
    public int rocketCount = 0;
    
    // Shield properties
    public int foodEaten = 0;
    public bool shield = false;
    public float shieldDuration = 5f;

    // GAME PROPERTIES =====================
    public float distance = 0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        foodEaten = 0;
    }

    void Update()
    {
        JumpGravity();

        if (foodEaten >= 3)
        {
            shield = true;
            foodEaten = 0;
            if (shieldDuration <= 5)
            {
                shieldDuration = 5f;
            }
        }

        if (shield)
        {
            shieldDuration -= Time.deltaTime;
            Debug.Log("Shield active...");
            if (shieldDuration <= 0)
            {
                shield = false;
                shieldDuration = 5f;
                Debug.Log("Shield deactivated...");
            }
        }
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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (rocketCount > 0)
            {
                rocketCount--;
                uicontrol.RocketUp(rocketCount);
                Debug.Log("Firing rocket...");
                RocketLaunch();
            }
        }
    }

    void RocketLaunch()
    {
        Vector3 pos = transform.position;
        pos.x += 1;
        Instantiate(rocket, pos, Quaternion.Euler(0, 0, 90));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Grounded");
            return;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            uicontrol.ScoreUp(5); // Call CoinUp method in UIController to update coin text
            return;
        }
        
        if (other.gameObject.tag == "Obstacle")
        {
            if (!invincible)
            {
                Debug.Log("Game Over");
            }

            if (shield)
            {
                Destroy(other.gameObject);
                uicontrol.ScoreUp(10);
            }
            return;
        }

        if (other.gameObject.tag == "Food")
        {
            Debug.Log("Eating...");
            foodEaten++;
            uicontrol.ScoreUp(10);
            return;
        }

        if (other.gameObject.tag == "Shield")
        {
            Debug.Log("Shield acquired...");
            shield = true;
            shieldDuration = 15f;
            uicontrol.ScoreUp(15);
            Destroy(other.gameObject);
            return;
        }

        if (other.gameObject.tag == "Rocket" && rocketCount < 4)
        {
            Debug.Log("Rocket acquired...");
            rocketCount++;
            uicontrol.RocketUp(rocketCount);
            uicontrol.ScoreUp(10);
            // Destroy(other.gameObject);
            return;
        }
    }
}
