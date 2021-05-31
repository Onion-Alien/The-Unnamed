using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    public GameObject g;
    public float groundCheckRadius;
    public Transform groundCheck;
    private LayerMask whatIsGround;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Player")
        {

            c.gameObject.GetComponent<PlayerController>().updateHealth(25,g);
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
    /*void OnTriggerEnter2D(Collider2D other)
   {
       if (other.name == "Player")
       {
           other.GetComponent<PlayerController>().updateHealth(25);
           Destroy(gameObject);
       }
   }*/
}
