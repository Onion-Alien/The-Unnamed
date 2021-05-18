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
        Idle,
        Pause
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
                case State.Pause:
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
            if (timer > 7f)
            {
                _state = State.Pause;
                arm_Spinner.doAttack2();
                yield return new WaitForSeconds(8f);
                arm_Smasher.doAttack1();
                yield return new WaitForSeconds(3f);
                arm_Smasher.doAttack1();
                yield return new WaitForSeconds(3f);
                arm_Smasher.doAttack1();
                timer = 0;
                _state = State.Idle;
            }
            yield return 0;
        }
    }

    private void Idle()
    {
        timer += 1 * Time.deltaTime;
    }
}
