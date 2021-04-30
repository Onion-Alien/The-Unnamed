using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
