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

    public GameObject spikes;

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
                case State.Seeking:
                    StartCoroutine("Seek");
                    break;
                case State.Attacking:
                    StartCoroutine("Attack2");
                    break;
                case State.Pause:
                    break;
            }
            yield return 0;
        }
    }

    private void Seek()
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
            _state = State.Pause;
            StartCoroutine(Wait(0.5f, State.Attacking));
        }
    }

    private Vector3 getOpposite()
    {
        return pos != left ? left : right;
    }
    private void Attack()
    {
        if (transform.position != getOpposite())
        {
            transform.position = Vector2.MoveTowards(transform.position, getOpposite(), 20 * Time.deltaTime);
        }
        if (transform.position == getOpposite())
        {
            StartCoroutine(Wait(0.5f, State.Idle));
            head._state = BOSS_Head.State.Idle;
            head.hasRunSpinner = false;
            hasRun = false;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = new Color(195f, 195f, 195f);
            }
        }
    }

    private void Attack2()
    {
        pos = center;
        if (transform.position != pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos, 20 * Time.deltaTime);
        }
        if (transform.position == pos)
        {
            StartCoroutine("Shoot");
            StartCoroutine(Wait(3f, State.Idle));
            head._state = BOSS_Head.State.Idle;
            head.hasRunSpinner = false;
            hasRun = false;
            foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>())
            {
                x.color = new Color(195f, 195f, 195f);
            }
        }
    }

    IEnumerator Shoot()
    {
        GameObject g = Instantiate(spikes, transform.position, transform.rotation);
        g.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Background";
        sr.sortingOrder = 5;
        rb.AddForce(transform.up * 500f);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }


    void Update()
    {
        if (_state == State.Attacking)
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
