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
    public int currentHealth;
    public HealthBar healthBar;
    private Rigidbody2D rb;
    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;
    public GameObject f5;
    public GameObject f6;
    public GameObject showDamage;

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
            case 5:
                f5.SetActive(true);
                Instantiate(f5, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 6:
                f4.SetActive(true);
                Instantiate(f6, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
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
        healthBar.showHP(currentHealth, maxHealth);
    }

    void ShowDamage(string text)
    {
        if (showDamage)
        {
            GameObject prefab = Instantiate(showDamage, new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public void TakeDamage(int damage)
    {
        ShowDamage(damage.ToString());

        currentHealth -= damage;
        healthBar.Set(currentHealth);
        healthBar.showHP(currentHealth, maxHealth);

        // play the hit animation if the enemy is hit

        if (currentHealth > 0)
        {
            animator.SetTrigger("isHit");
        }
        //Play the dead animation if the current health equals to or less than 0
        else if (currentHealth <= 0)
        {
            Die();
        }
    }


    //Set the trigger to playing the death animation to true and destroy the object 1 second after the Goblin is dead
    void Die()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (gameObject.name == "Boss")
        {
            animator.SetBool("isDead", true);
        }
        coin.SetActive(true);
        drop(Random.Range(1, 6));
        drop(Random.Range(1, 6));
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}