using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
<<<<<<< HEAD
    private Coroutine dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dmg = StartCoroutine(Damage(collision));
=======
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeRepeating("Damage", 0f, 1f);
>>>>>>> parent of 94ec4bd (Merge pull request #11 from phetrommer/Putheara)
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
<<<<<<< HEAD
        StopCoroutine(dmg);
    }

    private IEnumerator Damage(Collider2D col)
    {
        while (true)
        {
            if (col != null)
            {
                if (col.CompareTag("Player"))
                {
                    col.GetComponent<PlayerController>().TakeDamage(10, true);
                }
            }
            yield return new WaitForSeconds(1f);
=======
        CancelInvoke();
    }

    private void Damage()
    {
        if (player)
        {
            player.GetComponent<PlayerController>().TakeDamage(10);
>>>>>>> parent of 94ec4bd (Merge pull request #11 from phetrommer/Putheara)
        }
    }
}
