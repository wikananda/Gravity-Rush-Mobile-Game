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
    GameManager gameManager;

    Rigidbody2D rigid;
    Vector3 initialPos;
    float initialXPos;
    BoxCollider2D coll;
    PlayerRocket rocket;
    

    // ABILITY PROPERTIES ====================
    [SerializeField] float jumpForceGrounded = 10f;
    [SerializeField] float jumpForceAir = 20f;
    [SerializeField] float missileBounce = 80f;

    // GAME PROPERTIES =====================
    int gravityDirection = 1;
    [SerializeField] float gravityScale = 6.4106f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float rotationSpeed = 1f;
    // [SerializeField]float rotationDuration = 0.35f;
    // float rotationTime = 0f;

    // FOR REFERENCE (ANGLE ROTATION)
    float r;
    bool isJumping = false;
    float currentRigidRotation = 0f;
    Animator animator;

    // START & UPDATE ============================
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        shield = GetComponent<PlayerShield>();
        food = GetComponent<PlayerFood>();
        flash = GetComponent<PlayerFlash>();
        gameui = GameObject.Find("GameUI").GetComponent<GameUI>();
        rocket = GetComponent<PlayerRocket>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        initialPos = transform.position;
        initialXPos = initialPos.x;

        rigid.gravityScale = gravityScale;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation;

        if (initialPos.x != transform.position.x)
        {
            Vector3 targetPos = transform.position;
            targetPos.x = initialXPos;
            rigid.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
        }

        JumpGravity();

        // Always check for grounded
        if (IsGrounded())
        {
            animator.SetBool("Airborne", false);
            ResetJumpAnim();
        }
        else
        {
            animator.SetBool("Airborne", true);
        }

        // Rotating when jumping
        rigid.rotation = Mathf.Lerp(rigid.rotation, currentRigidRotation, rotationSpeed * Time.deltaTime);

        

        BetterFall(); // Make falling speed faster

        if (food.FoodEaten >= 3)
        {
            shield.ShieldOn(5f, 1);
            food.FoodEaten = 0;
        }

        if (gameManager.Speed > 10f && gameManager.Speed < 13f)
        {
            gameManager.Level = 2;
            missileBounce = 60f;
            return;
        }
        else if (gameManager.Speed > 13f && gameManager.Speed < 16f)
        {
            gameManager.Level = 3;
            missileBounce = 115f;
            return;
        }
        else if (gameManager.Speed > 16f)
        {
            gameManager.Level = 4;
            return;
        }
    }

    // METHODS ============================
    public void ResetJumpAnim()
    {
        isJumping = false;
        animator.SetBool("Jump", false);
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
                                                       Vector2.up * -gravityDirection,
                                                       0.15f,
                                                       LayerMask.GetMask("Ground"));
        bool grounded = raycastGround.collider != null;
        return grounded;
    }

    public void Rotate()
    {
        // Preparing for rotation
        Vector3 yRotate = new Vector3(0, gravityDirection * -180, 0);
        transform.Rotate(yRotate);
        currentRigidRotation += gravityDirection * 180;
    }

    void JumpGravity()
    {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Grounded: " + IsGrounded());
            gravityDirection *= -1;
            rigid.gravityScale *= -1;

            Rotate();
          
            if (flash.Flash)
            {
                flash.FlashMove(gravityDirection);
            }
            else if(IsGrounded())
            {
                // Debug.Log("Jumping");
                animator.SetBool("Jump", true);
                isJumping = true;
                rigid.AddForce(Vector3.up * -jumpForceGrounded * gravityDirection, ForceMode2D.Impulse);
                Debug.Log("Ground Jump");
            }
            else
            {
                // animator.SetBool("Airborne", true);
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

    // COLLISIONS ============================
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
            gameui.ScoreUp(10 * gameManager.Level);
            Destroy(collision.gameObject);
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameui.ScoreUp(5 * gameManager.Level); // Call CoinUp method in UIController to update coin text
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
                gameui.ScoreUp(10 * gameManager.Level);
                return;
            }
            else
            {
                gameManager.State = GameManager.GameState.GameOver;
                return;
            }
        }
    }
}
