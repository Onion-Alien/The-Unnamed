using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spike ball prop, deals damage on contact with player, used in Spike Ball Spawner script
 */

public class SpikeBall : MonoBehaviour
{

    SpikeBallSpawner sbs;
    public bool expire;

    //why does this give a null ref exception??????????????
    void Awake()
    {
        if (transform.parent.GetComponent<SpikeBallSpawner>().ballsExpire)
        {
            StartCoroutine("remove");
        }
    }

    private IEnumerator remove()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
