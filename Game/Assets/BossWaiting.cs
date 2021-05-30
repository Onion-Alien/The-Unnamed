using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaiting : MonoBehaviour
{
    private bool isIdle = true;
    private bool isWalking = false;
    public GameObject namePlate;
    public GameObject arenaWalls;
    public GameObject startBoss;
    public GameObject player;

    public bool fightActive = false;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isLeft = false;

    public float walkPoint;
    public float maxX, minX;

    public int movementSpeed;
    private int direction = 1;

    public float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        flip();
        startBoss.SetActive(false);
        arenaWalls.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = player.transform.position.x - gameObject.transform.position.x;

        if (!fightActive)
        {
            if (isWalking)
            {
                rb.velocity = new Vector2(movementSpeed * direction, 0.0f);

                if (direction == -1 && gameObject.transform.position.x <= walkPoint)
                {
                    rb.velocity = new Vector2(0.0f, 0.0f);
                    isWalking = false;
                    isIdle = true;
                }
                else if (direction == 1 && gameObject.transform.position.x >= walkPoint)
                {
                    rb.velocity = new Vector2(0.0f, 0.0f);
                    isWalking = false;
                    isIdle = true;
                }
            }
            if (playerDistance < 8.0f && playerDistance > -8.0f && player.transform.position.y < gameObject.transform.position.y + 4.0f && player.transform.position.y > gameObject.transform.position.y - 4.0f)
            {
                if (!isLeft)
                {
                    flip();
                }
                fightActive = true;
                isIdle = false;
                isWalking = false;
                gameObject.GetComponent<WardenAI>().fightActive = true;
                gameObject.GetComponent<WardenAI>().bossUpdate();
                arenaWalls.SetActive(true);
            }
            updateAnim();
        }
    }

    public void updateAnim()
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isWalking", isWalking);
    }

    //Flips sprite / text
    private void flip()
    {
        gameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
        namePlate.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void toggleIsLeft()
    {
        isLeft = !isLeft;
    }

    public void randomWalk()
    {
        if (!fightActive)
        {
            isIdle = false;
            walkPoint = Random.Range(minX, maxX);
            isWalking = true;
            updateAnim();

            if (gameObject.transform.position.x > walkPoint)
            {
                direction = -1;
                if (!isLeft)
                {
                    flip();
                    toggleIsLeft();
                }
            }
            else
            {
                direction = 1;
                if (isLeft)
                {
                    flip();
                    toggleIsLeft();
                }
            }
            rb.velocity = new Vector2(movementSpeed * direction, 0.0f);
        }
    }
}
