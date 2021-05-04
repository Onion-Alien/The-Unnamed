using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    private bool hasRun = false;
    Rigidbody2D rb;
    private Vector2 pos;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasRun)
        {
            hasRun = true;
            pos = new Vector2(transform.parent.position.x, transform.parent.position.y);
            StartCoroutine("Fall");
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(3f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(transform.up * -2);
        StartCoroutine(Replacement(rb));
    }

    private IEnumerator Replacement(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(3f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.position = pos;
        hasRun = false;
    }
}
