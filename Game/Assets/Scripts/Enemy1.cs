using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject FloatingDmgPointsPrefab; 
    public Animator animator;
    
    public int maxHealth = 100;
    int currentHealth;
    
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //Trigger floating damage text

        if (FloatingDmgPointsPrefab)
        {
            ShowFloatingDmgPoints();
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ShowFloatingDmgPoints
    {
        Instantiate(FloatingDmgPointsPrefab, transform.position, Quaternionn.identity,transform);
    }

    void Die()
    {
        animator.SetBool("isDead", true);

        Destroy(gameObject, 2);
        //this.enabled = false;
    }
}
