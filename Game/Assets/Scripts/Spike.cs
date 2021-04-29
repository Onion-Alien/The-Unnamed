using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Coroutine dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dmg = StartCoroutine(Damage(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
        }
    }
}
