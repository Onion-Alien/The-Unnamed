using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeRepeating("Damage", 0f, 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke();
    }

    private void Damage()
    {
        if (player)
        {
            player.GetComponent<PlayerController>().TakeDamage(10);
        }
    }
}
