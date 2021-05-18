using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Arm_Spinner : MonoBehaviour
{
    public Transform idlePoint;
    Vector3 randomPos;

    private Vector3 left, right, center;
    private Vector3 pos;
    public BOSS_Head head;
    private bool hasRun;
    private bool Attack1Running;
    public bool Attack2Running;
    private bool Attack2Shoot;

    public GameObject spikes;

    private State _state;
    public enum State
    {
        Idle,
        Attack1,
        Seek1,
        Attack2,
        Seek2,
        Pause
    };

    IEnumerator Start()
    {
        randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 1;
        left = GameObject.Find("Left").transform.position;
        right = GameObject.Find("Right").transform.position;
        center = GameObject.Find("Center").transform.position;
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
                case State.Seek2:
                    StartCoroutine("Seek2");
                    break;
                case State.Attack2:
                    StartCoroutine("Attack2");
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

    public void doAttack2()
    {
        if (!Attack2Running)
        {
            Attack2Running = true;
            _state = State.Seek2;
        }
    }

    private void Seek1()
    {
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            x.color = Color.red;
        }
        if (!hasRun)
        {
            pos = Random.Range(0f, 1f) < 0.4f ? left : right;
            hasRun = true;
        }
        if (transform.position != pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos, 20f * Time.deltaTime);
        }
        if (transform.position == pos)
        {
            _state = State.Attack1;
        }
    }

    private void Seek2()
    {
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            x.color = Color.red;
        }
        pos = center;
        if (transform.position != pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, center, 20f * Time.deltaTime);
        }
        if (transform.position == pos)
        {
            _state = State.Attack2;
        }
    }

    private void Attack1()
    {
        if (transform.position != getOpposite())
        {
            transform.position = Vector2.MoveTowards(transform.position, getOpposite(), 20 * Time.deltaTime);
        }
        if (transform.position == getOpposite())
        {
            StartCoroutine(Wait(0.5f, State.Idle));
            hasRun = false;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = new Color(195f, 195f, 195f);
            }
            Attack1Running = false;
        }
    }

    private void Attack2()
    {
        if (!Attack2Shoot)
        {
            Attack2Shoot = true;
            StartCoroutine("Shoot");
        }
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
        {
            x.color = new Color(195f, 195f, 195f);
        }
        StartCoroutine(Wait(5f, State.Idle));
        Attack2Running = false;
    }

    private Vector3 getOpposite()
    {
        return pos != left ? left : right;
    }

    IEnumerator Shoot()
    {
        GameObject g = Instantiate(spikes, transform.position, transform.rotation);
        g.AddComponent<Rigidbody2D>(); 
        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 0f;
        SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
        Spike spike = g.GetComponent<Spike>();
        sr.sortingLayerName = "Background";
        sr.sortingOrder = 5;
        sr.material.color = Color.red;
        spike.Launch(700f, 2f);
        yield return new WaitForSeconds(0.05f);
        Attack2Shoot = false;
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }

    void Update()
    {
        if (_state == State.Attack1)
        {
            transform.Rotate(0, 0, 1000 * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, 400 * Time.deltaTime);
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
}
