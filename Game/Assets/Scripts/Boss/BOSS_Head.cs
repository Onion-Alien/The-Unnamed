using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Head : MonoBehaviour
{
    private BOSS_Arm_Smasher arm_Smasher;
    private BOSS_Arm_Spinner arm_Spinner;
    public State _state;
    public Phase _phase;
    private float timer;
    public int runs;

    public int health;
    public HealthBar healthBar;

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

    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3
    };

    private void Awake()
    {
        arm_Smasher = GameObject.Find("Arm_Smasher").GetComponent<BOSS_Arm_Smasher>();
        arm_Spinner = GameObject.Find("Arm_Spinner").GetComponent<BOSS_Arm_Spinner>();

        health = 2000;
    }

    private void Update()
    {
        transform.position = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
    }

    IEnumerator Start()
    {
        _state = State.Idle;
        healthBar.SetMax(health);
        startPos = transform.position;
        _phase = Phase.Phase1;
        StartCoroutine(PhaseController());
        
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
            if (health <= 1900)
            {
                arm_Smasher.Death();
            }
            yield return 0;
        }
    }

    IEnumerator Wait(float i, State state)
    {
        yield return new WaitForSeconds(i);
        _state = state;
    }

    private IEnumerator PhaseController()
    {
        while (health > 0)
        {
            yield return new WaitForSeconds(1f);
            switch (_phase)
            {
                case Phase.Phase1:
                    StartCoroutine("Phase1");
                    break;
                case Phase.Phase2:
                    StartCoroutine("Phase2");
                    break;
            }
            if (health >= 1500)
            {
                _phase = Phase.Phase1;
            }
            if (health >= 1000 && health < 1500)
            {
                _phase = Phase.Phase2;
            }
        }
    }

    private void Phase1()
    {
        switch(Mathf.CeilToInt(timer))
        { 
            case 10:
                arm_Smasher.doSpecial();
                //arm_Spinner.doAttack1();
                break;
            case 15:
                arm_Smasher.doAttack1();
                break;
            case 18:
                arm_Smasher.doAttack1();
                break;
            case 21:
                arm_Smasher.doAttack1();
                break;
            case 28:
                arm_Spinner.doAttack2();
                break;
            case 35:
                break;
            case 40:
                timer = 0;
                _state = State.Idle;
                break;
        }
    }

    private void Phase2()
    {
        switch (Mathf.CeilToInt(timer))
        {

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.Set(health);
        if (health <= 0)
        {
            //Die();
        }
    }

    private void Idle()
    {
        timer += 1 * Time.deltaTime;
    }
}
