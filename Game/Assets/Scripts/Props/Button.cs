using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool reusable;
    private bool hasRun = false;
    public float force;
    public float decayTime;
    public float multipleObjectInterval;
    public bool SpikeButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasRun)
        {
            if (SpikeButton)
            {
                if (collision.tag == "Player")
                {
                    hasRun = true;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.0f, -0.1f), 1f * Time.deltaTime);
                    StartCoroutine(ForcePush(multipleObjectInterval));
                }
            }
            if (reusable)
            {
                hasRun = false;
            }
        }
    }

    private IEnumerator ForcePush(float multipleObjectInterval)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Spike>().Launch(force, decayTime);
            yield return new WaitForSeconds(multipleObjectInterval);
        }
    }
}
