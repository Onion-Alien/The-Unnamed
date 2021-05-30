using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBossCombat : MonoBehaviour
{
	public int attackDamage = 20;

	public float attackRange = 1f;
	public LayerMask attackMask;
	public Transform attackPoint;

	public void Attack()
	{
		//conduct the attack and reduce player's HP once the player reaches the attack point
		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerController>().TakeDamage(attackDamage, false);
		}
	}

}
