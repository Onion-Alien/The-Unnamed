using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopKeeper;
    public GameObject player;
    public GameObject merchantText;
    public GameObject shopCanvas;
    public Text merchantMessage;

    private Rigidbody2D rb;
    private Animator anim;

    public static bool shopActive = false;
    private bool isIdle = true;
    private bool isWalking = false;
    private bool playerInProx = false;
    private bool isLeft = false;

    private float walkPoint;
    public float maxX, minX;

    public int movementSpeed;
    private int direction = 1;

    void Start()
    {
        shopCanvas.gameObject.SetActive(false);
        merchantText.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        merchantText.transform.position = new Vector3(shopKeeper.transform.position.x, shopKeeper.transform.position.y + 1.6f, shopKeeper.transform.position.z);

        //Checks if player is close and if so, stops shopkeeper from walking and displays the text
        if (player.transform.position.x < shopKeeper.transform.position.x + 2.5f && player.transform.position.x > shopKeeper.transform.position.x - 2.5f && player.transform.position.y < shopKeeper.transform.position.y + 1.0f && player.transform.position.y > shopKeeper.transform.position.y - 1.0f)
        {
            if (!shopActive)
            {
                merchantText.SetActive(true);
            }
            rb.velocity = new Vector2(0.0f, 0.0f);
            isWalking = false;
            isIdle = true;
            playerInProx = true;
            
        }

        if (playerInProx)
        {
            if (!(player.transform.position.x < shopKeeper.transform.position.x + 2.0f && player.transform.position.x > shopKeeper.transform.position.x - 2.0f && player.transform.position.y < shopKeeper.transform.position.y + 3.0f && player.transform.position.y > shopKeeper.transform.position.y - 1.0f))
            {
                playerInProx = false;
                merchantText.SetActive(false);
                if (shopActive)
                {
                    toggleShop();
                }
            }
        }

        //Checks current location against the point he is walking to, to see if its time to stop walking
        if (isWalking)
        {
            if (direction == -1 && shopKeeper.transform.position.x <= walkPoint)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isIdle = true;
                isWalking = false;
            }
            else if(direction == 1 && shopKeeper.transform.position.x >= walkPoint)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isIdle = true;
                isWalking = false;
            }
        }
        updateAnim();
    }

    //Gets random position to walk to
    public void randomWalk()
    {
        if (!playerInProx && isIdle == true)
        {
            isIdle = false;
            isWalking = true;
            updateAnim();
            walkPoint = Random.Range(minX, maxX);

            if (shopKeeper.transform.position.x > walkPoint)
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

    //Updates animator component
    public void updateAnim()
    {
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("shopActive", playerInProx);
    }

    //Called from walk animation as an event to ensure the character doesn't move until the animation starts
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
        shopKeeper.transform.Rotate(0.0f, 180.0f, 0.0f);
        merchantText.transform.Rotate(0.0f, 180.0f, 0.0f); 
    }

    public void toggleIsLeft()
    {
            isLeft = !isLeft;
    }

    //Toggles the shop window canvas
    public void toggleShop()
    {

        if (!shopActive && playerInProx)
        {
            merchantText.SetActive(false);
            shopCanvas.SetActive(true);
            shopActive = true;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            shopCanvas.SetActive(false);
            merchantText.SetActive(true);
            shopActive = false;
            StartCoroutine(exitMessage());
        }
    }

    IEnumerator exitMessage()
    {
        string temp = merchantMessage.text;
        merchantMessage.text = "Your business is appreciated, please stop by again soon.";
        yield return new WaitForSeconds(5.0f);
        merchantMessage.text = temp;
    }
}
