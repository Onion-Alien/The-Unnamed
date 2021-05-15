using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    public enum enemyType { GOBLIN, SKELETON, MUSHROOM, FLYINGEYE };
    public enemyType current;

    public float groundCheckRadius;
    public Transform groundCheck;
    private LayerMask whatIsGround;


    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.name == "Player")
        {
           
                if (current == enemyType.GOBLIN)
                {
                 GoldCount.gc.addGold(70);
                 Destroy(gameObject);
            }
                else if (current == enemyType.MUSHROOM)
                {
                GoldCount.gc.addGold(80);
                Destroy(gameObject);
            }
                else if (current == enemyType.SKELETON)
                {
                GoldCount.gc.addGold(100);
                Destroy(gameObject);
            }
                else if (current == enemyType.FLYINGEYE)
                {
                GoldCount.gc.addGold(50);
                Destroy(gameObject);
            }

               
       
        }
    }

    private void Start()
    {


        whatIsGround = LayerMask.GetMask("Ground", "ignoreGround");

    }

    public bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
