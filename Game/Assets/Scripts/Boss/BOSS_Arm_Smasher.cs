using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BOSS_Arm_Smasher : MonoBehaviour
{
    public Transform idlePoint;
    private Vector3 randomPos;
    private GameObject player;

    private Vector3 attackPos;
    private Vector3 trackingPos;
    public BOSS_Head head;

    public bool attack1Running;
    public bool attackSpecialRunning;
    public bool attackSpecial;

    public State state;
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
        
        state = State.Idle;

        while (true)
        {
            switch (state)
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
        if (!attack1Running)
        {
            attack1Running = true;
            state = State.Seek1;
        }
    }

    public void doSpecial()
    {
        if (!attackSpecialRunning)
        {
            attackSpecialRunning = true;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = Color.green;

            }
            state = State.SeekSpecial;
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
            state = State.Pause;
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
            state = State.Pause;
            StartCoroutine(Wait(0.5f, State.Attack1));
            attackPos = new Vector3(trackingPos.x, trackingPos.y - 5.7f);
        }
    }

    private void AttackSpecial()
    {
        if (transform.position != attackPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos, 80 * Time.deltaTime);
        }
        if (transform.position == attackPos)
        {
            StartCoroutine(Wait(5f, State.Idle));
            attackSpecialRunning = false;
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
            attack1Running = false;
        }
    }

    public void TakeDamage(int damage)
    {
        head.TakeDamage(damage);
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        this.state = state;
    }
}
