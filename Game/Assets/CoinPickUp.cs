using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

   /* public enum enemyType { GOBLIN, SKELETON, MUSHROOM, FLYINGEYE };
    public enemyType current;*/

    public float groundCheckRadius;
    public Transform groundCheck;
    private LayerMask whatIsGround;
    public int r1;
    public int r2;
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.name == "Player")
        {     
                GoldCount.gc.addGold(Random.Range(r1, r2));
                Destroy(gameObject);
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
