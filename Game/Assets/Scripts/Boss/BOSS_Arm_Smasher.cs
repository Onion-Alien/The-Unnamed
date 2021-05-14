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

    public State _state;
    public enum State
    {
        Seeking,
        Idle,
        Attacking,
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
                case State.Seeking:
                    StartCoroutine("Seek");
                    break;
                case State.Attacking:
                    StartCoroutine("Attack");
                    break;
                case State.Pause:
                    break;
            }
            yield return 0;
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

    private void Seek()
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
            StartCoroutine(Wait(0.5f, State.Attacking));
            attackPos = new Vector3(trackingPos.x, trackingPos.y - 5.3f);
        }
    }

    private void Attack()
    {
        if (transform.position != attackPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos, 100 * Time.deltaTime);
        }
        if (transform.position == attackPos)
        {
            StartCoroutine(Wait(0.2f, State.Idle));
            head._state = BOSS_Head.State.Idle;
            head.hasRunSmasher = false;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = new Color(195, 195, 195);
            }
        }
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }
}
