using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            player.transform.position = new Vector3(132.0f, 160.0f, 0.0f);
        }
    }
}

