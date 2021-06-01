using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spike ball prop, deals damage on contact with player, used in Spike Ball Spawner script
 */

public class SpawnedAttributes : MonoBehaviour
{
    void Awake()
    {
        Spawner spawner = GetComponentInParent<Spawner>();
        // if (spawner.ignoreGround)
        // {
        //     GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        //     Collider2D c1 = ground.GetComponent<Collider2D>();
        //     Collider2D c2 = gameObject.GetComponent<Collider2D>();
        //     Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), c1);
        // }
        if (spawner.isExpire)
        {
            StartCoroutine("remove");
        }
    }

    private IEnumerator remove()
    {
        yield return new WaitForSeconds(GetComponentInParent<Spawner>().expireTime);
        Destroy(gameObject);
    }
}
