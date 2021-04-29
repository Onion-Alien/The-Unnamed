using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public Animator animator;
    public LayerMask enemyLayers;
    public LayerMask movableLayers;

    public int DMG_light = 20;
    public int DMG_medium = 30;
    public int DMG_heavy = 40;
    public float attackRange = 0.5f;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Light();
                nextAttackTime = Time.time + 0.5f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Medium();
                nextAttackTime = Time.time + 0.5f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                Heavy();
                nextAttackTime = Time.time + 0.5f / attackRate;
            }
        }
    }
    
    //turn this into a case or something
    void Light()
    {
        animator.SetTrigger("ATK_Light");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDeathScript>().TakeDamage(DMG_light);
        }
    }

    void Medium()
    {
        animator.SetTrigger("ATK_Medium");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDeathScript>().TakeDamage(DMG_medium);
        }
    }

    void Heavy()
    {
        animator.SetTrigger("ATK_Heavy");
        StartCoroutine(Damage(DMG_heavy));
        StartCoroutine("moveObject");
    }
    
    private IEnumerator moveObject()
    {
        Collider2D[] hitMovables = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, movableLayers);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D movable in hitMovables)
        {
            movable.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000000f);
        }
    }

    private IEnumerator Damage(int dmg)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDeathScript>().TakeDamage(dmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
