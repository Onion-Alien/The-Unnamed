using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spike prop, deals damage on contact with player
 */

public class Spike : MonoBehaviour
{
    private Coroutine dmg;
    public int spikeDmg = 50;

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
                    col.GetComponent<PlayerController>().TakeDamage(spikeDmg, true);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void Launch(float force, float decayTime)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        StartCoroutine(Remove(decayTime));
    }

    private IEnumerator Remove(float decayTime)
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
