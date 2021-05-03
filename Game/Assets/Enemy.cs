
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is used to ensure that the
 * enemy always faces the player
 */

public class Enemy : MonoBehaviour
{
	public Transform player;

	public bool isFlipped = true;

	//Make the enemy face different direction according to player pos
	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

}