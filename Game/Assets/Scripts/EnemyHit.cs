using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        CheckIdle();
    }

    // checks if enemy is idle
    void CheckIdle()
    {
        if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f)
        {
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // play the hit animation if the Goblin is hit


        if (currentHealth > 0)
            {

            animator.SetTrigger("isHit");

            }
      
        //Set the animation to Idle
        
        //Play the dead animation if the current health equals to or less than 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Set the trigger to playing the death animation to true and destroy the object 2 seconds after the Goblin is dead
    void Die()
    {
        animator.SetBool("isIdle", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("isDead", true);
        GetComponent<Animator>().Play(gameObject.name + "_Dead", 0, 0f);
        Destroy(gameObject, 2);
        //this.enabled = false;
    }
}
