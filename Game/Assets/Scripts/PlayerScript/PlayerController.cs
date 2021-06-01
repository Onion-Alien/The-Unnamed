using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This script controls the player controller
 */

public class PlayerController : MonoBehaviour
{ 
    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool canJump;
    public bool isDead;
    public bool isFrozen;
    public bool isBlocking;

    private Rigidbody2D rb;
    private Animator anim; 

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;

    public int amountOfJumps = 1;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Transform groundCheck;
    private LayerMask whatIsGround;
    public GameObject gameOverScreen;
    private PlayerCombat playerCombat;
    public GameObject showDamage;

    private float horizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        healthBar.SetMax(maxHealth);
        amountOfJumpsLeft = amountOfJumps;
        whatIsGround = LayerMask.GetMask("Ground", "ignoreGround");
        gameOverScreen.SetActive(false);
        healthBar.showHP(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (!PauseMenuManager.isPaused)
        {
            if (!isDead)
            {
                if (!isBlocking)
                {
                    rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
                }
                CheckIfCanJump();
                UpdateAnimations();
                CheckMovementDirection();
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            anim.SetTrigger("isJumping");
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                amountOfJumpsLeft--;
            }
            if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            if (context.performed)
            {
                if (playerCombat.stamina >= 20)
                {
                    StartCoroutine(playerCombat.UseStamina(20f));
                    isBlocking = true;
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    isFrozen = true;
                }
            }
            if (context.canceled)
            {
                isBlocking = false;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isFrozen = false;
            }
        }
    }

    //checks if player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); 
    }
    
    //checks if player can jump and if they have any more jumps left
    private void CheckIfCanJump()
    {
        if(IsGrounded() && rb.velocity.y <= 1)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        
        if(amountOfJumpsLeft <= 0)
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
        if(isFacingRight && horizontal < 0)
        {
            Flip();
        }
        else if(!isFacingRight && horizontal > 0)
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
        anim.SetBool("isGrounded", IsGrounded());
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isBlocking", isBlocking);
    }
    //handles controller inputs for functions other than movement

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
        ShowDamage(damage.ToString());
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
        healthBar.showHP(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Freezes the player, used for after attacks so you can't move and spam attacks
    public void Freeze()
    {
        //StartCoroutine("freezeTime");
    }

    private IEnumerator freezeTime()
    {
        isFrozen = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFrozen = false;

    }

    public void Die()
    {
        isDead = true;
        anim.SetBool("isWalking", false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("isDead", true);
        gameOverScreen.SetActive(true);
       // gameOverScreen.GetComponent<GameOverScreen>().Setup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            if (collision.name == "JumpPowerup")
            {
                Destroy(collision.gameObject);
                StartCoroutine(tripleJump());
            }
            else if (collision.name == "DamagePowerup")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleDamage());
            }
            else if (collision.name == "HealthPowerup")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleHealth());
            }
            else if (collision.name == "MoveSpeedPowerup")
            {
                Destroy(collision.gameObject);
                StartCoroutine(movespeedBuff());
            }
            else if (collision.name == "StaminaPowerup")
            {
                Destroy(collision.gameObject);
                StartCoroutine(doubleStamina());
            }
            else if (collision.name == "AtkSpeedPowerup")
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
        yield return new WaitForSeconds(10.0f);
        amountOfJumps = amountOfJumps - 1;
    }

    IEnumerator doubleDamage()
    {
        HudScript.instance.activateDamage();
        playerCombat.dmgLight *= 2;
        playerCombat.dmgHeavy *= 2;
        yield return new WaitForSeconds(10.0f);
        playerCombat.dmgLight /= 2;
        playerCombat.dmgHeavy /= 2;
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
        yield return new WaitForSeconds(10.0f);
        movementSpeed = movementSpeed / 1.5f;
    }

    IEnumerator doubleStamina()
    {
        HudScript.instance.activateStamina();
        playerCombat.setStamina(playerCombat.getStamina() * 2);
        playerCombat.setStaminaRegen(playerCombat.getStaminaRegen() * 2);
        yield return new WaitForSeconds(10.0f);
        playerCombat.setStamina(playerCombat.getStamina() / 2);
        playerCombat.setStaminaRegen(playerCombat.getStaminaRegen() / 2);
    }

    IEnumerator attackSpeedBuff()
    {
        HudScript.instance.activateAtkSpeed();
        playerCombat.setAttackRate(playerCombat.getAttackRate() * 2);
        yield return new WaitForSeconds(10.0f);
        playerCombat.setAttackRate(playerCombat.getAttackRate() / 2);
    }

    public void updateHealth(int value, GameObject item)
    {
        if (item.name.Contains("RedPotion"))
        {    
            if ((currentHealth + value ) <= maxHealth)
            {
                currentHealth += value;
                healthBar.Set(currentHealth);
            }
            else
            {
                currentHealth = maxHealth;
                healthBar.Set(maxHealth);
            }
        }
    }

    void ShowDamage(string text)
    {
        if (showDamage)
        {
            GameObject prefab = Instantiate(showDamage, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
