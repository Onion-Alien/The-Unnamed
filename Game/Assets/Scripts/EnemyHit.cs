using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // play the hit animation if the Goblin is hit


        if (currentHealth > 0)
            {

                GetComponent<Animator>().Play(gameObject.name + "_Hit", 0, 0f);
            animator.SetBool("isIdle", true);

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
        animator.SetBool("isDead", true);

        Destroy(gameObject, 2);
        //this.enabled = false;
    }
}
