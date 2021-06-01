using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is used by the enemy to perform attack and crease the
 * player's health bar once the attack is executed
 */

public class EnemyAttack : MonoBehaviour
{

	public int attackDamage = 8;

	public float attackRange = 1f;
	public LayerMask attackMask;
	public Transform attackPoint;

	public void Attack()
	{
		//conduct the attack and reduce player's HP once the player reaches the attack point
		Collider2D[] colInfo = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);
		foreach (Collider2D x in colInfo)
		{
			PlayerController pc = x.GetComponent<PlayerController>();
			if (pc != null)
			{
				pc.TakeDamage(attackDamage, true);
			}
		}
	}
}




