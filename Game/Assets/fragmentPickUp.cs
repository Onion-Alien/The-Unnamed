using UnityEngine;

public class fragmentPickUp : MonoBehaviour
{
    public GameObject g;
    public float groundCheckRadius;
    public Transform groundCheck;
    private LayerMask whatIsGround;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            FragmentCount.addFragment(1, g);
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