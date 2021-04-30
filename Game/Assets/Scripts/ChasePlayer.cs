using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public float speed = 2.5f;
    public float attackRange = 3f;
    private Vector2 movement;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.GetComponent<EnemyFlip>().LookAtPlayer();
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }

    private void FixedUpdate()
    {
        
        moveEnemy(movement);
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack");
            animator.GetComponent<EnemyAttack>().Heavy();
        }
    }
    void moveEnemy(Vector2 direction)
    {
        
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
