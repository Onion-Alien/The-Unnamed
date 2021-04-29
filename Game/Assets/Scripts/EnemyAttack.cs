using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public LayerMask PlayerLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int dmg = 10;

    public float attackRate = 0.5f;


    void Heavy()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PlayerLayers);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(dmg);
        }
    }
}
