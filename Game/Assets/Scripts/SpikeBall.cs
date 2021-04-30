using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spike ball prop, deals damage on contact with player, used in Spike Ball Spawner script
 */

public class SpikeBall : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("remove");
    }

    private IEnumerator remove()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
