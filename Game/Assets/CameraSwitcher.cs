using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private bool hasRun = false;
    public GameObject cam;
    public bool Enable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasRun)
        {
            if (collision.tag == "Player")
            {
                if (enabled)
                {
                    cam.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        hasRun = true;
    }
}
