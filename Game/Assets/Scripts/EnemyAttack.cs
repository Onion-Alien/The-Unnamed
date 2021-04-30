using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

	public int attackDamage = 20;
	public int enragedAttackDamage = 40;

	public float attackRange = 1f;
	public LayerMask attackMask;
	public Transform attackPoint;

	public void Attack()
	{

		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerController>().TakeDamage(attackDamage, true);
		}
	}


}
