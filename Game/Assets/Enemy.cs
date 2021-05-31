
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is used to ensure that the
 * enemy always faces the player
 */

public class Enemy : MonoBehaviour
{
	public bool isFlipped = true;
	public Vector2 spawnPos;
	public Transform returnpoint;

	public float groundCheckRadius;
	public Transform groundCheck;
	private LayerMask whatIsGround;

	//semi broken
	private void Start()
    {
		returnpoint = gameObject.transform.Find("Return Point");
		//spawnPos = new Vector2(returnpoint.position.x, returnpoint.position.y);
		whatIsGround = LayerMask.GetMask("Ground", "ignoreGround");
		transform.localScale = new Vector3(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), 1);
	}

	public bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}

	//Make the enemy face different direction according to player pos
	public void LookAt(Transform pos)
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > pos.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < pos.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	public bool RangeCheck(Animator animator, Transform player, Rigidbody2D rb)
	{
		float yDistance = player.position.y - rb.position.y;
		float xDistance = player.position.x - rb.position.x;

		if (IsGrounded())
		{
			if (xDistance <= 12 && xDistance >= -12)
			{
				if (yDistance < 3 && yDistance > -3)
				{
					return true;
				}
			}
		}
		return false;
	}

}