using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public float groundCheckRadius;
    public Transform groundCheck;
    private LayerMask whatIsGround;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            SaveManager.instance.GoldCount += Random.Range(1, 5);
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