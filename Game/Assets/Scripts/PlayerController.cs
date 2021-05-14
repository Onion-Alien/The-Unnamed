using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script controls the player controller
 */

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;

    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    public bool isGrounded;
    private bool canJump;
    public bool isDead = false;
    public bool isFrozen = false;
    public bool isBlocking = false;

    private Rigidbody2D rb;
    private Animator anim;

    public static float movementSpeed = 8.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;

    public int amountOfJumps = 1;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private PlayerCombat playerCombat;
    public GameOverScreen gameOverScreen;
    public GameObject gameOverScreenObject;

    void Start()
    {
        gameOverScreenObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar.SetMax(maxHealth);
        amountOfJumpsLeft = amountOfJumps;
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        if (!PauseMenuManager.isPaused)
        {
            if (!isDead)
            {
                CheckInput();
                CheckMovementDirection();
                CheckIfCanJump();
                UpdateAnimations();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            ApplyMovement();
            CheckSurroundings();
        }
    }
    //checks if player is on the ground
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    //checks if player can jump and if they have any more jumps left
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isBlocking", isBlocking);
    }
    //handles controller inputs for functions other than movement
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.P))
        {
            Die();
        }


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(playerCombat.UseStamina(20f));
                isBlocking = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isFrozen = true;

            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isBlocking = false;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isFrozen = false;
            }
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }

    }

    private void ApplyMovement()
    {
        if (!isBlocking)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (!isFrozen)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void TakeDamage(int damage, bool ignoreBlock)
    {
        if (!isDead)
        {
            if (isBlocking && !ignoreBlock)
            {
                anim.SetTrigger("isBlock");
                currentHealth -= (int)(damage * 0.1);
            }
            else
            {
                anim.SetTrigger("isHit");
                currentHealth -= damage;
            }
        }
        healthBar.Set(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //Freezes the player, used for after attacks so you can't move and spam attacks
    public void Freeze()
    {
        StartCoroutine("freezeTime");
    }

    private IEnumerator freezeTime()
    {
        isFrozen = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFrozen = false;

    }

    private void Die()
    {
        isDead = true;
        anim.SetBool("isWalking", false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("isDead", true);

        gameOverScreen.Setup();
        gameOverScreenObject.SetActive(true);
        //handle death events here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            if (collision.name == "TripleJump")
            {
                Destroy(collision.gameObject);
                StartCoroutine(tripleJump());
            }
            else if(collision.name == "DoubleDamage")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleDamage());
            }
            else if (collision.name == "DoubleHealth")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleHealth());
            }
            else if (collision.name == "MoveSpeedBuff")
            {
                Destroy(collision.gameObject);
                StartCoroutine(movespeedBuff());
            }
            else if (collision.name == "StaminaBuff")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleStamina());
            }
            else if (collision.name == "AttackSpeedBuff")
            {
                Destroy(collision.gameObject);
                StartCoroutine(attackSpeedBuff());
            }
        }
    }

    IEnumerator tripleJump()
    {
        HudScript.instance.activateJumpPowerup();
        amountOfJumps = amountOfJumps + 1;
        yield return new WaitForSeconds(5.0f);
        amountOfJumps = amountOfJumps - 1;
    }

    IEnumerator doubleDamage()
    {
        HudScript.instance.activateDamage();
        int[] temp = PlayerCombat.getDMG();
        for (int i = 0; i <= 2; i++)
        {
            temp[i] = temp[i] * 2;
        }
        PlayerCombat.setDMG(temp);
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i <= 2; i++)
        {
            temp[i] = temp[i] / 2;
        }
        PlayerCombat.setDMG(temp);
    }

    IEnumerator doubleHealth()
    {
        HudScript.instance.activateHealthPowerup();
        //Czn't find player hp variable (believe it doesn't exist on this variant for some reason)
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator movespeedBuff()
    {
        HudScript.instance.activateMSpeed();
        movementSpeed = movementSpeed * 1.5f;
        yield return new WaitForSeconds(5.0f);
        movementSpeed = movementSpeed / 1.5f;
    }

    IEnumerator doubleStamina()
    {
        HudScript.instance.activateStamina();
        PlayerCombat.setStamina(PlayerCombat.getStamina() * 2);
        PlayerCombat.setStaminaRegen(PlayerCombat.getStaminaRegen() * 2);
        yield return new WaitForSeconds(5.0f);
        PlayerCombat.setStamina(PlayerCombat.getStamina() / 2);
        PlayerCombat.setStaminaRegen(PlayerCombat.getStaminaRegen() / 2);
    }

    IEnumerator attackSpeedBuff()
    {
        HudScript.instance.activateAtkSpeed();
        PlayerCombat.setAttackRate(PlayerCombat.getAttackRate() * 2);
        yield return new WaitForSeconds(5.0f);
        PlayerCombat.setAttackRate(PlayerCombat.getAttackRate() / 2);
    }
}
