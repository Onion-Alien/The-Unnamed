using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * creates a spawner which spawns x amount of balls once you pass through a trigger
 */

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float waitTime = 1f;
    public int spawnCount = 1;
    public bool ignoreGround;
    public bool isExpire;
    public int expireTime;
    public bool isRepeating;
    public bool isPlatform;
    public float gravityScale;
    public bool isHurting;
    public bool alwaysActive;

    public void Spawn()
    {
        StartCoroutine(spawnBalls(waitTime));
    }

    private IEnumerator spawnBalls(float waitTime)
    {
        do
        {
            for (int k = 0; k < spawnCount; k++)
            {
                if (waitTime > 0f)
                {
                    Transform trigger = gameObject.transform.Find("Spawn Point");
                    GameObject g = Instantiate(enemy, new Vector2(trigger.position.x, trigger.position.y), Quaternion.identity, transform);
                    g.AddComponent<SpawnedAttributes>();
                    if (isPlatform)
                    {
                        g.AddComponent<Rigidbody2D>();
                        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                        rb.drag = 2;
                        rb.mass = 100000;
                        rb.gravityScale = gravityScale;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }
                    if (isHurting)
                    {
                        if (g.GetComponent<Spike>() == null)
                        {
                            g.AddComponent<Spike>();
                        }
                    }
                    if (ignoreGround)
                    {
                        g.layer = 11;
                    }
                }
                yield return new WaitForSeconds(waitTime);
            }
        } while (isRepeating);
    }
}
