using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    GameUI gameui;
    PlayerShield shield;
    PlayerFood food;
    PlayerFlash flash;

    Rigidbody2D rigid;
    Vector3 initialPos;
    float initialXPos;
    BoxCollider2D coll;
    PlayerRocket rocket;
    

    // ABILITY PROPERTIES ====================
    public float jumpForceGrounded = 10f;
    public float jumpForceAir = 20f;
    public float speed = 7f;
    public float maxSpeed = 14f;
    public float acceleration = 1f;
    
    // STATE PROPERTIES =======================
    public int level = 1;
    
    // Missile properties
    public float missileBounce = 80f;

    // GAME PROPERTIES =====================
    int gravityDirection = 1;
    public float gravityScale = 6.4106f;
    public float distance = 0f;
    public float fallMultiplier = 2.5f;

    // GAME STATE ENUM ====================
    public enum GameState
    {
        Playing,
        GameOver
    }

    GameState state;
    void Start()
    {
        Application.targetFrameRate = 60;

        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        shield = GetComponent<PlayerShield>();
        food = GetComponent<PlayerFood>();
        flash = GetComponent<PlayerFlash>();
        gameui = GameObject.Find("GameUI").GetComponent<GameUI>();
        rocket = GetComponent<PlayerRocket>();

        initialPos = transform.position;
        initialXPos = initialPos.x;
        level = 1;

        rigid.gravityScale = gravityScale;
        state = GameState.Playing;
    }

    // UPDATE ============================
    void Update()
    {
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
            rigid.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
        }

        JumpGravity();
        
        // Make falling speed faster
        BetterFall();

        speed += acceleration * Time.deltaTime / 20f;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (food.FoodEaten >= 3)
        {
            shield.ShieldOn(5f, 1);
            food.FoodEaten = 0;
        }

        if (speed > 10f && speed < 13f)
        {
            level = 2;
            missileBounce = 60f;
            return;
        }
        else if (speed > 13f)
        {
            level = 3;
            missileBounce = 115f;
            return;
        }
    }

    void BetterFall()
    {
        if (rigid.velocity.y < 0 && gravityDirection > 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigid.velocity.y > 0 && gravityDirection < 0)
        {
            rigid.velocity += Vector2.down * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastGround = Physics2D.BoxCast(coll.bounds.center, 
                                                       coll.bounds.size,
                                                       0f,
                                                       Vector2.up * gravityDirection,
                                                       0.15f,
                                                       LayerMask.GetMask("Ground"));
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
            
            if (flash.Flash)
            {
                flash.FlashMove(gravityDirection);
            }
            else if(IsGrounded())
            {
                rigid.AddForce(Vector3.up * -jumpForceGrounded * gravityDirection, ForceMode2D.Impulse);
                Debug.Log("Ground Jump");
            }
            else
            {
                rigid.gravityScale = 0;
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpForceAir * gravityDirection, ForceMode2D.Impulse);
                rigid.gravityScale = gravityScale * gravityDirection;
                Debug.Log("Air Jump");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (rocket.RocketCount > 0)
            {
                rocket.RocketCount--;
                gameui.RocketUp(rocket.RocketCount);
                rocket.RocketLaunch();
            }
        }
    }

    public static float dist;
    void GameOver()
    {
        Debug.Log("Game Over");
        dist = distance;
        Debug.Log(dist);
        SceneManager.LoadScene("GameOver");
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
            gameui.ScoreUp(10 * level);
            Destroy(collision.gameObject);
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameui.ScoreUp(5 * level); // Call CoinUp method in UIController to update coin text
            return;
        }
        
        if (other.gameObject.tag == "Obstacle")
        {
            if (shield.Shield)
            {
                shield.ShieldCount--;
                Destroy(other.gameObject);
                string objectName = other.gameObject.name;
                Debug.Log("Destroyed with shield : " + objectName);
                gameui.ScoreUp(10 * level);
                return;
            }
            else
            {
                state = GameState.GameOver;
                return;
            }
        }
    }
}
