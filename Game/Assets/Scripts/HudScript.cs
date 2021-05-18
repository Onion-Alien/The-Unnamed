using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScript : MonoBehaviour
{
    public GameObject damagePowerup, staminaPowerup, atkSpeedPowerup, mSpeedPowerup, jumpPowerup, healthPowerup;
    public static HudScript instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);

        damagePowerup.SetActive(false); staminaPowerup.SetActive(false); atkSpeedPowerup.SetActive(false); mSpeedPowerup.SetActive(false); jumpPowerup.SetActive(false); healthPowerup.SetActive(false);
    }

    // Update is called once per frame

    public void activateDamage()
    {
        StartCoroutine(damage());
    }

    IEnumerator damage()
    {
        damagePowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        damagePowerup.SetActive(false);
    }

    public void activateStamina()
    {
        StartCoroutine(stamina());
    }

    IEnumerator stamina()
    {
        staminaPowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        staminaPowerup.SetActive(false);
    }

    public void activateAtkSpeed()
    {
        StartCoroutine(atkSpeed());
    }

    IEnumerator atkSpeed()
    {
        atkSpeedPowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        atkSpeedPowerup.SetActive(false);
    }

    public void activateMSpeed()
    {
        StartCoroutine(mSpeed());
    }

    IEnumerator mSpeed()
    {
        mSpeedPowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        mSpeedPowerup.SetActive(false);
    }

    public void activateJumpPowerup()
    {
        StartCoroutine(jump());
    }

    IEnumerator jump()
    {
        jumpPowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        jumpPowerup.SetActive(false);
    }

    public void activateHealthPowerup()
    {
        StartCoroutine(health());
    }

    IEnumerator health()
    {
        healthPowerup.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        healthPowerup.SetActive(false);
    }
}