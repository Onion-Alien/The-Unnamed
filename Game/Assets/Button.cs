using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private Spike spike;

    private void Awake()
    {
        spike = GetComponentInChildren<Spike>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.0f, -0.052f), 1f * Time.deltaTime);
            spike.Launch();
        }
    }
}
