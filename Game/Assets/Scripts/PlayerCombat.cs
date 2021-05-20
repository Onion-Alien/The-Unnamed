using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This script contains all the functions for player combat
 */

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public Animator animator;
    public LayerMask enemyLayers;
    public LayerMask movableLayers;

    private static int DMG_light = 20;
    private static int DMG_medium = 30;
    private static int DMG_heavy = 40;
    private float attackRange = 0.5f;

    private static float attackRate = 0.5f;
    float nextAttackTime = 0f;

    private float stamina = 100f;
    private static float maxStamina = 100f;
    private static float StaminaRegenTimer = 1f;
    private const float StaminaDecreasePerFrame = 1f;
    private const float StaminaIncreasePerFrame = 35;
    private const float StaminaTimeToRegen = 1f;
    public HealthBar stamBar;

    private PlayerController pc;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Start()
    {
        stamBar.SetMax(Mathf.RoundToInt(maxStamina));
    }

    void Update()
    {
        if (!pc.isDead)
        {
            StaminaUpdate();
        }
    }
    //calculates current stamina and how long until stamina can regen based on time
    private void StaminaUpdate()
    {
        if (stamina < maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (StaminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina); //sets stamina based on delta time
                stamBar.Set(Mathf.RoundToInt(stamina)); //rounds to int because hp bar needs floats
            }
            else
            {
                StaminaRegenTimer += Time.deltaTime;
            }
        }
    }

    public IEnumerator UseStamina(float stamCost)
    {
        yield return new WaitForSeconds(0.2f);
        stamina -= stamCost;
        stamBar.Set(Mathf.RoundToInt(stamina));
        StaminaRegenTimer = 0.0f;
    }

    public IEnumerator SetStamina(float stam)
    {
        yield return new WaitForSeconds(0.2f);
        stamina += stam;
        stamBar.Set(Mathf.RoundToInt(stamina));
        StaminaRegenTimer = 0.0f;
    }

    //player light attack
    public void Light(InputAction.CallbackContext context)
    {
        if (pc.IsGrounded() && Time.time >= nextAttackTime && stamina >= 40 && !pc.isBlocking)
        {
            animator.SetTrigger("ATK_Light");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHit>().TakeDamage(DMG_light);
            }
            StartCoroutine(UseStamina(20f));
            pc.Freeze();
            nextAttackTime = Time.time + 0.5f / attackRate;
        }
    }
    //player medium attack
    public void Medium(InputAction.CallbackContext context)
    {
        if (pc.IsGrounded() && Time.time >= nextAttackTime && stamina >= 20 && !pc.isBlocking)
        {
            animator.SetTrigger("ATK_Medium");
            StartCoroutine(Damage(DMG_medium));
            StartCoroutine(moveObject("ATK_Medium"));
            StartCoroutine(UseStamina(20f));
            pc.Freeze();
            nextAttackTime = Time.time + 0.5f / attackRate;
        }
    }
    //player heavy attack which also is enabled to move certain objects
    public void Heavy(InputAction.CallbackContext context)
    {
        if (pc.IsGrounded() && Time.time >= nextAttackTime && stamina >= 40 && !pc.isBlocking)
        {
            animator.SetTrigger("ATK_Heavy");
            StartCoroutine(Damage(DMG_heavy));
            StartCoroutine(moveObject("ATK_Heavy"));
            StartCoroutine(UseStamina(40f));
            pc.Freeze();
            nextAttackTime = Time.time + 0.5f / attackRate;
        }
    }

    //allows the player to move certain objects with a heavy attack
    private IEnumerator moveObject(string attack)
    {
        Collider2D[] hitMovables = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, movableLayers);

        if (hitMovables.Length != 0)
        {
            StartCoroutine(SetStamina(40f));
        }
        yield return new WaitForSeconds(0.1f);
        switch(attack)
        {
            case "ATK_Heavy":
            {
                foreach (Collider2D movable in hitMovables)
                {
                    movable.GetComponent<Rigidbody2D>().AddForce(transform.up * 500000f);
                    movable.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000000f);
                }
                break;
            }
            case "ATK_Medium":
            {
                foreach (Collider2D movable in hitMovables)
                {
                    movable.GetComponent<Rigidbody2D>().AddForce(transform.up * 500000f);
                    movable.GetComponent<Rigidbody2D>().AddForce(transform.right *- 1000000f);
                }
                break;
            }
        }

    }

    private IEnumerator Damage(int dmg)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHit>().TakeDamage(dmg);
            enemy.GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(200f, 500f));
            enemy.GetComponent<Rigidbody2D>().AddForce(transform.right * Random.Range(200f, 500f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public static void setDMG(int[] DMG)
    {
        DMG_light = DMG[0];
        DMG_medium = DMG[1];
        DMG_heavy = DMG[2];
    }

    public static void setAttackRate(float rate)
    {
        attackRate = rate;
    }

    public static void setStamina(float stamina)
    {
        maxStamina = stamina;
    }

    public static void setStaminaRegen(float stamRegen)
    {
        StaminaRegenTimer = stamRegen;
    }

    public static int[] getDMG()
    {
        return new int[3] { DMG_light, DMG_medium, DMG_heavy };
    }

    public static float getAttackRate()
    {
        return attackRate;
    }

    public static float getStamina()
    {
        return maxStamina;
    }

    public static float getStaminaRegen()
    {
        return StaminaRegenTimer ;
    }
}
