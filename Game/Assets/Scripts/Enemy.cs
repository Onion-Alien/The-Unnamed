
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

	//semi broken
	private void Start()
    {
		returnpoint = gameObject.transform.Find("Return Point");
		spawnPos = new Vector2(returnpoint.position.x, returnpoint.position.y);
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

}