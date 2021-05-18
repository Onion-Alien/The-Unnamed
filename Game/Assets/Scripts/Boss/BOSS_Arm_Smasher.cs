using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Arm_Smasher : MonoBehaviour
{
    public Transform idlePoint;
    Vector3 randomPos;
    private GameObject player;

    private Vector3 attackPos;
    private Vector3 trackingPos;
    public BOSS_Head head;

    public bool Attack1Running;

    public State _state;
    public enum State
    {
        Seek1,
        Idle,
        Attack1,
        Pause
    };

    IEnumerator Start()
    {
        randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 1;
        player = GameObject.FindGameObjectWithTag("Player");
        head = GameObject.Find("Head").GetComponent<BOSS_Head>();
        
        _state = State.Idle;

        while (true)
        {
            switch (_state)
            {
                case State.Idle:
                    StartCoroutine("Idle");
                    break;
                case State.Seek1:
                    StartCoroutine("Seek1");
                    break;
                case State.Attack1:
                    StartCoroutine("Attack1");
                    break;
                case State.Pause:
                    break;
            }
            yield return 0;
        }
    }

    public void doAttack1()
    {
        if (!Attack1Running)
        {
            Attack1Running = true;
            _state = State.Seek1;
        }
    }

    private void Idle()
    {
        if (transform.position != randomPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPos, 10f * Time.deltaTime);
        }
        if (transform.position == randomPos)
        {
            randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 1;
        }
    }

    private void Seek1()
    {
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            x.color = Color.red;
        }
        trackingPos = new Vector3(player.transform.position.x, player.transform.position.y + 6f);
        if (transform.position != trackingPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, trackingPos, 40f * Time.deltaTime);
        }
        if (transform.position == trackingPos)
        {
            _state = State.Pause;
            StartCoroutine(Wait(0.5f, State.Attack1));
            attackPos = new Vector3(trackingPos.x, trackingPos.y - 5.7f);
        }
    }

    private void Attack1()
    {
        if (transform.position != attackPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos, 80 * Time.deltaTime);
        }
        if (transform.position == attackPos)
        {
            StartCoroutine(Wait(0.5f, State.Idle));
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = new Color(195, 195, 195);
            }
            Attack1Running = false;
        }
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }
}
