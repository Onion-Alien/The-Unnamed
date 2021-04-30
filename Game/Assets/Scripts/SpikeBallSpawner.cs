using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * creates a spike ball spawner which spawns x amount of balls once you pass through a trigger
 */

public class SpikeBallSpawner : MonoBehaviour
{
    public GameObject enemy;
    public float waitTime = 1f;
    public int spawnCount = 1;
    private bool hasRun = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasRun)
        {
            hasRun = true;
            StartCoroutine(spawnBalls(waitTime));
        }
    }

    private IEnumerator spawnBalls(float waitTime)
    {
        for (int k = 0; k < spawnCount; k++)
        {
            if (waitTime > 0f)
            {
                Instantiate(enemy, new Vector2(transform.parent.position.x, transform.parent.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
