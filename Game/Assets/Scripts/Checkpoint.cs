using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script controls the checkpoint functions
 */

public class Checkpoint : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        GetComponent<SpriteRenderer>().color = Color.yellow;
        SaveManager.instance.checkPointSave(player);
    }
}
