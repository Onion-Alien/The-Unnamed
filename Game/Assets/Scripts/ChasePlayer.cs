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

    public LayerMask PlayerLayers;
    public Transform attackPoint;
    public int dmg = 2;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

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
        if (Time.time >= nextAttackTime)
        {
            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                Heavy();
                nextAttackTime = Time.time + 0.5f / attackRate;
            }
        }
        else
        {
            moveEnemy(movement);
        }
    }
    void moveEnemy(Vector2 direction)
    {
        
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public void Heavy()
    {
        animator.SetTrigger("attack");

        player.GetComponent<PlayerController>().TakeDamage(dmg, true);
    }
}
