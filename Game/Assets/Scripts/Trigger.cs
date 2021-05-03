using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool hasRun;
    Spawner sbs;

    private void Awake()
    {
        sbs = GetComponentInParent<Spawner>();
        if (sbs.alwaysActive)
        {
            sbs.Spawn();
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasRun)
        {
            hasRun = true;
            sbs.Spawn();
        }
    }
}
