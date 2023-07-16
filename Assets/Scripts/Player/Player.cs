using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIController uicontrol;
    [SerializeField] GameObject rocket;

    Rigidbody2D rigid;
    Vector3 initialPos;
    float initialXPos;
    BoxCollider2D coll;

    // ABILITY PROPERTIES ====================
    int gravityDirection = 1;
    public float jumpForceGrounded = 10f;
    public float jumpForceAir = 20f;
    public float speed = 7f;
    public float maxSpeed = 14f;
    public float acceleration = 1f;
    public float coinMagnetSpeed = 5f;

    // STATE PROPERTIES =======================
    public int level = 1;
    public bool coinMagnet = false;
    public bool flash = false; // Flash movement when gravity change
    public int rocketCount = 0;
    // Missile properties
    public float missileBounce = 80f;
    // Shield properties
    public int foodEaten = 0;
    public bool shield = false;
    public int shieldCount = 1;
    public float shieldDuration = 5f;

    // GAME PROPERTIES =====================
    public float distance = 0f;

    // GAME STATE ENUM ====================
    public enum GameState
    {
        Playing,
        GameOver
    }

    GameState state;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        foodEaten = 0;
        initialPos = transform.position;
        initialXPos = initialPos.x;
        level = 1;
        jumpForceGrounded = 15f;
        jumpForceAir = 40f;
        rigid.gravityScale = 10.4106f;
        state = GameState.Playing;
    }

    void Update()
    {
        // Debug.Log(jumpForceGrounded * level * 0.7f);
        // Debug.Log(jumpForceAir * level);
        if (state == GameState.GameOver)
        {
            GameOver();
            return;
        }

        distance += speed * Time.deltaTime / 1.2f;

        if (initialPos.x != transform.position.x)
        {
            Vector3 targetPos = transform.position;
            targetPos.x = initialXPos;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
        }

        JumpGravity();
        speed += acceleration * Time.deltaTime / 20f;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (foodEaten >= 3)
        {
            shield = true;
            foodEaten = 0;
            if (shieldDuration <= 5)
            {
                shieldDuration = 5f;
                shieldCount = 1;
            }
        }

        if (shield)
        {
            shieldDuration -= Time.deltaTime;
            Debug.Log("Shield active...");
            if (shieldDuration <= 0)
            {
                shieldCount = 1;
                shieldDuration = 5f;
                shield = false;
                Debug.Log("Shield deactivated...");
            }
        }

        if (speed > 10f && speed < 13f)
        {
            level = 2;
            // rigid.gravityScale = 10 * gravityDirection;
            missileBounce = 60f;
            jumpForceAir = 50f;
            rigid.gravityScale = 11.4106f * gravityDirection;
            // jumpForceGrounded = 25f;
        }
        else if (speed > 13f)
        {
            level = 3;
            // rigid.gravityScale = 13 * gravityDirection;
            missileBounce = 115f;
            jumpForceAir = 70f;
            rigid.gravityScale = 12.4106f * gravityDirection;
            jumpForceGrounded = 20f;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastGround = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up * gravityDirection, 0.15f, LayerMask.GetMask("Ground"));
        Color rayColor;
        if (raycastGround.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(coll.bounds.center, Vector2.up * gravityDirection * 1f, rayColor);
        Debug.Log(raycastGround.collider);
        bool grounded = raycastGround.collider != null;
        return grounded;
    }

    void JumpGravity()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            gravityDirection *= -1;
            rigid.gravityScale *= -1;
            
            if (flash)
            {
                Vector3 upPos = GameObject.Find("UpPos").transform.position;
                Vector3 downPos = GameObject.Find("DownPos").transform.position;

                if (gravityDirection < 0)
                {
                    transform.position = new Vector3(transform.position.x, upPos.y, 0);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, downPos.y, 0);
                }

                shieldDuration = 1f;
                shield = true; // Give mini shield during teleporting
                // rigid.AddForce(Vector3.up * -jumpForceGrounded * 300 * gravityDirection, ForceMode2D.Impulse);
            }
            // A workaround for the floating feel problem when jumping
            else if(IsGrounded())
            {
                rigid.AddForce(Vector3.up * -jumpForceGrounded * gravityDirection, ForceMode2D.Impulse);
                Debug.Log("Ground Jump");
            }
            else
            {
                rigid.AddForce(Vector3.up * -jumpForceAir * gravityDirection, ForceMode2D.Impulse);
                Debug.Log("Air Jump");
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

    void GameOver()
    {
        Debug.Log("Game Over");

        if (speed > 0.1)
        {
            speed -= acceleration * Time.deltaTime * 15f * level;
        }
        else
        {
            speed = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            Vector3 normal = (transform.position - collision.transform.position);
            normal.x = 0;
            normal.z = 0;
            if (normal.y > 0)
            {
                normal.y = 1;
            }
            else if (normal.y < 0)
            {
                normal.y = -1;
            }
            rigid.AddForce(normal * missileBounce, ForceMode2D.Impulse);
            uicontrol.ScoreUp(10 * level);
            Destroy(collision.gameObject);
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            uicontrol.ScoreUp(5 * level); // Call CoinUp method in UIController to update coin text
            return;
        }
        
        if (other.gameObject.tag == "Obstacle")
        {
            if (shield)
            {
                shieldCount--;
                if(shieldCount == 0){
                    shield = false;
                    shieldDuration = 5f;
                }
                Destroy(other.gameObject);
                string objectName = other.gameObject.name;
                Debug.Log("Destroyed with shield : " + objectName);
                uicontrol.ScoreUp(10 * level);
            }else{
                state = GameState.GameOver;
            }
            return;
        }

        if (other.gameObject.tag == "Food")
        {
            Debug.Log("Eating...");
            foodEaten++;
            uicontrol.ScoreUp(10 * level);
            return;
        }

        if (other.gameObject.tag == "Shield")
        {
            Debug.Log("Shield acquired...");
            shield = true;
            shieldDuration = 15f;
            shieldCount = 1;
            uicontrol.ScoreUp(15 * level);
            Destroy(other.gameObject);
            return;
        }

        if (other.gameObject.tag == "Rocket" && rocketCount < 4)
        {
            Debug.Log("Rocket acquired...");
            rocketCount++;
            uicontrol.RocketUp(rocketCount);
            uicontrol.ScoreUp(10 * level);
            Destroy(other.gameObject);
            return;
        }
    }
}
