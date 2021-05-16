using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script will be used to play the hit animation
 * once the enemy is hit by the player
 */

public class EnemyHit : MonoBehaviour
{

    public Animator animator;
    public GameObject coin;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    private Rigidbody2D rb;
    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;


     void drop(int i)
    {
        switch (i)
        {
            case 1:
                f1.SetActive(true);
                Instantiate(f1, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 2:
                f2.SetActive(true);
                Instantiate(f2, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 3:
                f3.SetActive(true);
                Instantiate(f3, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 4:
                f4.SetActive(true);
                Instantiate(f4, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            default:
                break;
        }

    }
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMax(maxHealth);
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
        healthBar.Set(currentHealth);
        // play the hit animation if the enemy is hit

        if (currentHealth > 0)
        {
            animator.SetTrigger("isHit");
        }

        //Play the dead animation if the current health equals to or less than 0
        if (currentHealth <= 0)
        {
            Die();
            
        }
    }

    
        
    //Set the trigger to playing the death animation to true and destroy the object 1 second after the Goblin is dead
    void Die()
    {
        animator.SetBool("isIdle", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("isDead", true);
        coin.SetActive(true);
        drop(Random.Range(1, 4));
        drop(Random.Range(1, 4));
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
        
        
    }
}
