using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Head : MonoBehaviour
{
    private BOSS_Arm_Smasher arm_Smasher;
    private BOSS_Arm_Spinner arm_Spinner;
    public State _state;
    private float timer;
    public int runs;

    public int health;
    public bool hasRun;
    public bool hasRunSmasher;
    public bool hasRunSpinner;

    private Vector3 startPos;

    public float speed = 1;
    public float xScale = 1;
    public float yScale = 1;
    public enum State
    {
        Arm_Smasher,
        Arm_Spinner,
        Idle
    };

    private void Awake()
    {
        arm_Smasher = GameObject.Find("Arm_Smasher").GetComponent<BOSS_Arm_Smasher>();
        arm_Spinner = GameObject.Find("Arm_Spinner").GetComponent<BOSS_Arm_Spinner>();
        health = 500;
    }

    private void Update()
    {
        transform.position = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
    }

    IEnumerator Start()
    {
        _state = State.Idle;
        startPos = transform.position;
        StartCoroutine(Phase1());
        
        while (true)
        {
            switch (_state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Arm_Smasher:
                    StartCoroutine("Arm_Smasher");
                    break;
                case State.Arm_Spinner:
                    StartCoroutine("Arm_Spinner");
                    break;
            }
            yield return 0;
        }
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }

    private IEnumerator Phase1()
    {
        while (health >= 450)
        {
            if (timer > 5f)
            {
                StartCoroutine(Wait(0f, State.Arm_Spinner));
                //StartCoroutine(Wait(0f, State.Arm_Smasher));
                yield return new WaitForSeconds(2f);
                StartCoroutine(Wait(0f, State.Arm_Smasher));
                yield return new WaitForSeconds(2f);
                StartCoroutine(Wait(0f, State.Arm_Smasher));
                yield return new WaitForSeconds(5f);

                timer = 0;
            }
            yield return 0;
        }
    }

    private void Idle()
    {
        timer += 1 * Time.deltaTime;
    }

    private void Arm_Smasher()
    {
        if (!hasRunSmasher)
        {
            arm_Smasher._state = BOSS_Arm_Smasher.State.Seeking;
            hasRunSmasher = true;
        }
    }

    private void Arm_Spinner()
    {
        if (!hasRunSpinner)
        {
            arm_Spinner._state = BOSS_Arm_Spinner.State.Seeking;
            hasRunSpinner = true;
        }
    }
}
