using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Region : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(99999, true);
        }
    }
}
