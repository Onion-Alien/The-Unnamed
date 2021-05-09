using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineBehaviour
{
	public float speed = 2.5f;

	Transform player;
	Rigidbody2D rb;
	Enemy enemy;
	EnemyAttack enemyAttack;
	private bool isAttacking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		enemy = animator.GetComponent<Enemy>();
		enemyAttack = animator.GetComponent<EnemyAttack>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		float yDistance = player.position.y - rb.position.y;
		float xDistance = player.position.x - rb.position.x;

		if (Vector2.Distance(player.position, rb.position) <= enemyAttack.attackRange)
		{
			animator.SetTrigger("attack");
			isAttacking = true;
		}

		// only tracks player if within x and y range
		if (enemy.IsGrounded() && !isAttacking)
        {
			if (xDistance < 20 && xDistance > -20)
			{
				if (yDistance < 3 && yDistance > -3)
				{
					enemy.LookAt(player);
					Vector2 target = new Vector2(player.position.x, rb.position.y);
					Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
					rb.MovePosition(newPos);
				}
			}
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("attack");
		isAttacking = false;
	}
}
