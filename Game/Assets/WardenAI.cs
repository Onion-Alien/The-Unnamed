using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenAI : MonoBehaviour
{
    private int hp = 200;

    public GameObject arenaWalls;
    public GameObject namePlate;
    public GameObject fire;
    public GameObject healthbarObj;

    public GameObject player;
    private Rigidbody2D rb;

    private bool attack1 = false;
    private bool attack2 = false;
    private bool attack3 = false;
    private bool isFrozen = false;
    private bool isRunning = false;
    private bool isInvis = false;
    private bool isJumping = false;
    private bool isGrounded = false;
    private bool isIdle = true;
    private bool isDead = false;
    public bool fightActive = false;

    private Animator anim;

    public Transform attackPoint;

    public Animator animator;
    public LayerMask enemyLayers;
    public float attackRange;

    public static float movementSpeed = 4.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;

    public int amountOfJumps = 1;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Transform groundCheck;
    private LayerMask whatIsGround;

    private float invisValue = 1.0f;
    public int attackMove;
    public float playerDistance;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        attackMove = Random.Range(1, 3);
        whatIsGround = LayerMask.GetMask("Ground", "ignoreGround");
        isIdle = false;
        bossUpdate();
    }

    private void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (anim.GetBool("isDead") == true)
        {
            isDead = true;
            isRunning = false;
            updateAnim();
            arenaWalls.SetActive(false);
          //  gameObject.SetActive(false);
        }

        if (isInvis)
        {
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.a = invisValue;
            gameObject.GetComponent<SpriteRenderer>().color = temp;
            if(invisValue > 0.05f)
            {
                invisValue -= 0.005f;
            }
            else
            {
                namePlate.SetActive(false);
                fire.SetActive(false);
                healthbarObj.SetActive(false);
            }
        }
        else if(invisValue < 1.0f)
        {
            invisValue += 0.01f;
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.a = invisValue;
            gameObject.GetComponent<SpriteRenderer>().color = temp;
            namePlate.SetActive(true);
            fire.SetActive(true);
            healthbarObj.SetActive(true);
        }
    }

    private void die()
    {
        gameObject.SetActive(false);
    }

    private void runVelocity() 
    {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void checkIfCanAtk()
    {
        if (playerDistance < 2.0f && playerDistance > -2.0f)
        {
            bossUpdate();
        }
    }

    private void updateDirection()
    {
        playerDistance = player.transform.position.x - gameObject.transform.position.x;
        if (playerDistance > 0 && movementSpeed < 0)
        {
            movementSpeed = movementSpeed * -1.0f;
            flip();
        }
        else if (playerDistance < 0 && movementSpeed > 0)
        {
            movementSpeed = -movementSpeed;
            flip();
        }
    }

    public void bossUpdate()
    {
        updateDirection();
            
            if (playerDistance < 3.0f && playerDistance > -3.0f)
            {
                isRunning = false;
                attackPlayer();
            }
            else if (playerDistance < 10.0f && playerDistance > -10.0f)
            {
                runToPlayer();
            }
            else if (fightActive)
            {

                int random = Random.Range(1, 3);
                isRunning = false;

                if (random == 2)
                {
                    goInvis();
                    runToPlayer();
                }
                else
                {
                    if (checkGrounded())
                    {
                        isRunning = false;
                        Jump();
                    }
                    else
                    {
                        StartCoroutine(delayBossUpdate());
                    }
                }
            }
            else
            {
                StartCoroutine(delayBossUpdate());
            }
            updateAnim();
        
    }

    public void Jump()
    {
        rb.velocity = new Vector2(movementSpeed * 3, jumpForce);
        isJumping = true;
        isGrounded = false;
    }

    public bool checkGrounded()
    {
        if(gameObject.transform.position.y < 118.5f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        return isGrounded;
    }

    public void runToPlayer()
    {
        updateDirection();
        isRunning = true;
    }

    public void attackPlayer()
    {
        isInvis = false;
        
        if(attackMove != 3)
        {
            attackMove += Random.Range(1, 2);
        }
        else
        {
            attackMove = 1;
        }


        if (attackMove == 1)
        {
            attack1 = true;
        }
        else if (attackMove == 2)
        {
            attack2 = true;
        }
        else
        {
            attack3 = true;
        }
    }

    private void flip()
    {
        gameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
        namePlate.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void goInvis()
    {
        StartCoroutine(invisTimer());
    }

    private IEnumerator invisTimer()
    {
        isInvis = true;
        yield return new WaitForSeconds(5.0f);
        isInvis = false;
    }

    private IEnumerator hitPlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D enemy in hitEnemies)
        {
            player.GetComponent<PlayerController>().TakeDamage(Random.Range(20, 40), false);

            //Player hit animation
        }
    }

    public void Freeze()
    {
        StartCoroutine(freezeTime());
    }

    private IEnumerator freezeTime()
    {
        isFrozen = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(1.0f);
        rb.constraints = RigidbodyConstraints2D.None;
        isFrozen = false;
        updateDirection();
        bossUpdate();
    }

    private IEnumerator delayBossUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        updateDirection();
        bossUpdate();
    }

    public void setAttackFalse()
    {
        attack1 = false; 
        attack2 = false; 
        attack3 = false;
        updateAnim();
    }

    public void updateAnim()
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("attack2", attack2);
        anim.SetBool("attack1", attack1);
        anim.SetBool("attack3", attack3);
        anim.SetBool("isFrozen", isFrozen);
        anim.SetBool("isInvis", isInvis);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isRunning", isRunning);
    }

    public void jumpLanding()
    {
        isJumping = false;
        updateAnim();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("playercollisionenter");
        if (collision.name == "PlayerNew")
        {

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("playercollisionexit");
        if (collision.name == "PlayerNew")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
