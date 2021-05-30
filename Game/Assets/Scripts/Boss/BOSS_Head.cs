using System.Collections;
using UnityEngine;

public class BOSS_Head : MonoBehaviour
{
    private BOSS_Arm_Smasher armSmasher;
    private BOSS_Arm_Spinner armSpinner;
    public State _state;
    public Phase _phase;
    public float timer;

    public int health;
    public HealthBar healthBar;

    private Vector3 startPos;

    public float speed = 1;
    public float xScale = 1;
    public float yScale = 1;
    public enum State
    {
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
        armSmasher = GameObject.Find("Arm_Smasher").GetComponent<BOSS_Arm_Smasher>();
        armSpinner = GameObject.Find("Arm_Spinner").GetComponent<BOSS_Arm_Spinner>();

        health = 2000;
    }

    private void Update()
    {
        transform.position = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
    }

    private IEnumerator Start()
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
            yield return 0;
        }
    }

    private IEnumerator Wait(float i, State state)
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
                    StopCoroutine("Phase1");
                    StartCoroutine("Phase2");
                    break;
                case Phase.Phase3:
                    StopCoroutine("Phase2");
                    StartCoroutine("Phase3");
                    break;
            }
            if (health >= 1500)
            {
                _phase = Phase.Phase1;
            }
            if (health >= 750 && health < 1500)
            {
                _phase = Phase.Phase2;
            }
            if (health < 750)
            {
                _phase = Phase.Phase3;
            }
        }
    }

    private void Phase1()
    {
        switch(Mathf.CeilToInt(timer))
        { 
            case 10:
                armSpinner.doAttack1();
                break;
            case 15:
                armSmasher.doAttack1();
                break;
            case 18:
                armSmasher.doAttack1();
                break;
            case 21:
                armSmasher.doAttack1();
                break;
            case 28:
                armSpinner.doAttack2();
                break;
            case 35:
                armSmasher.doSpecial();
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
            case 10:
                armSmasher.doAttack1();
                break;
            case 13:
                armSmasher.doAttack1();
                break;
            case 16:
                armSmasher.doAttack1();
                break;
            case 19:
                armSmasher.doAttack1();
                break;
            case 25:
                armSpinner.doAttack2();
                break;
            case 30:
                armSmasher.doAttack1();
                break;
            case 35:
                armSmasher.doSpecial();
                break;
            case 40:
                timer = 0;
                _state = State.Idle;
                break;
        }
    }
    
    private void Phase3()
    {
        switch (Mathf.CeilToInt(timer))
        {
            case 10:
                armSmasher.doAttack1();
                break;
            case 13:
                armSmasher.doAttack1();
                break;
            case 16:
                armSmasher.doAttack1();
                break;
            case 19:
                armSmasher.doAttack1();
                break;
            case 25:
                armSpinner.doAttack2();
                break;
            case 30:
                armSmasher.doAttack1();
                break;
            case 35:
                armSmasher.doSpecial();
                break;
            case 40:
                timer = 0;
                _state = State.Idle;
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.Set(health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }

    private void Idle()
    {
        timer += 1 * Time.deltaTime;
    }
}
