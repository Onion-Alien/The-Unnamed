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
    private bool isLaunch;

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
        if (col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().TakeDamage(spikeDmg, true);
            }
        }
        yield return new WaitForSeconds(1f);
    }

    private void Update()
    {
        if (isLaunch)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 10);
        }     
    }

    public void Launch(float force, float decayTime)
    {
        isLaunch = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        StartCoroutine(Remove(decayTime));
    }

    private IEnumerator Remove(float decayTime)
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
