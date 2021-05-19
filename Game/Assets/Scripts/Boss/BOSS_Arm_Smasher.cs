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
    public bool AttackSpecialRunning;
    public bool attackSpecial;

    public State _state;
    public enum State
    {
        Seek1,
        Idle,
        Attack1,
        Pause,
        AttackSpecial,
        SeekSpecial
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
                case State.SeekSpecial:
                    StartCoroutine("SeekSpecial");
                    break;
                case State.Attack1:
                    StartCoroutine("Attack1");
                    break;
                case State.AttackSpecial:
                    StartCoroutine("AttackSpecial");
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

    public void doSpecial()
    {
        if (!AttackSpecialRunning)
        {
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = Color.green;
            }
            foreach (Spike x in GetComponentsInChildren<Spike>())
            {
                x.enabled = false;
            }
            AttackSpecialRunning = true;
            _state = State.SeekSpecial;
        }
    }

    private void Idle()
    {
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            x.color = new Color(195, 195, 195);
        }
        if (transform.position != randomPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPos, 10f * Time.deltaTime);
        }
        if (transform.position == randomPos)
        {
            randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 1;
        }
    }

    private void SeekSpecial()
    {
        trackingPos = new Vector3(player.transform.position.x, player.transform.position.y + 6f);
        if (transform.position != trackingPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, trackingPos, 40f * Time.deltaTime);
        }
        if (transform.position == trackingPos)
        {
            _state = State.Pause;
            attackPos = new Vector3(trackingPos.x, trackingPos.y - 5.7f);
            StartCoroutine(Wait(1f, State.AttackSpecial));
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
            Attack1Running = false;
        }
    }

    public void TakeDamage(int damage)
    {
        head.TakeDamage(damage);
    }

    private void AttackSpecial()
    {
        if (transform.position != attackPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos, 80 * Time.deltaTime);
        }
        if (transform.position == attackPos)
        {
            Invoke("EnableSpikes", 5f);
            StartCoroutine(Wait(5f, State.Idle));
            AttackSpecialRunning = false;
        }
    }

    private void EnableSpikes()
    {
        foreach (Spike x in GetComponentsInChildren<Spike>())
        {
            x.enabled = true;
        }
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }

    public void Death()
    {
        foreach (Rigidbody2D x in GetComponentsInChildren<Rigidbody2D>())
        {
            x.constraints = RigidbodyConstraints2D.None;
        }
        gameObject.SetActive(false);
    }
}
