using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script controls the checkpoint functions
 */

public class Checkpoint : MonoBehaviour
{
    public Vector2 currentCheckpoint;
    private bool hasRun = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasRun)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            currentCheckpoint = transform.position;
        }
    }
}
