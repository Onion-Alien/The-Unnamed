using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Enemy enemy;
    public float speed = 2.5f;
    EnemyAttack enemyAttack;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        enemyAttack = animator.GetComponent<EnemyAttack>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= enemyAttack.attackRange)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            if (enemy.RangeCheck(animator, player, rb))
            {
                enemy.LookAt(player);
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.SetBool("isRunning", false);
    }
}
