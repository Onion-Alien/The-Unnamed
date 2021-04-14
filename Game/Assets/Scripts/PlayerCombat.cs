using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public Animator animator;
    public LayerMask enemyLayers;

    public int DMG_atk1 = 20;
    public int DMG_atk2 = 30;
    public int DMG_atk3 = 40;
    public float attackRange = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack1();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack2();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack3();
        }
    }
    
    //turn this into a case or something
    void Attack1()
    {
        animator.SetTrigger("Attack1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(DMG_atk1);
        }
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(DMG_atk2);
        }
    }
    void Attack3()
    {
        animator.SetTrigger("Attack3");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(DMG_atk3);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
