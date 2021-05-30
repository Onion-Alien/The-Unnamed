using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    private bool isIdle = true;
    private bool isWalking = false;
    public GameObject namePlate;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isLeft = false;

    private float walkPoint;
    public float maxX, minX;

    public int movementSpeed;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAnim();
        if (isWalking)
        {
            if (direction == -1 && gameObject.transform.position.x <= walkPoint)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isIdle = true;
                isWalking = false;
            }
            else if (direction == 1 && gameObject.transform.position.x >= walkPoint)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isIdle = true;
                isWalking = false;
            }
        }
    }

    public void updateAnim()
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isWalking", isWalking);
    }

    public void startWalking()
    {
        if (!isIdle)
        {
            rb.velocity = new Vector2(movementSpeed * direction, 0.0f);
        }
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
        isIdle = false;
        isWalking = true;
        walkPoint = Random.Range(minX, maxX);

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
    }

}