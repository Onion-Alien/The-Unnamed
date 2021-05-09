using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script contains all the functions for player combat
 */

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public Animator animator;
    public LayerMask enemyLayers;
    public LayerMask movableLayers;

    public int DMG_light = 20;
    public int DMG_medium = 30;
    public int DMG_heavy = 40;
    public float attackRange = 0.5f;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    public float stamina = 100f;
    public float maxStamina = 100f;
    private float StaminaRegenTimer = 1f;
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
            InputCheck();
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
                StaminaRegenTimer += Time.deltaTime; //
            }
        }
    }
    //handles input
    private void InputCheck()
    {
        if (Time.time >= nextAttackTime)
        {
            if (pc.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.L) && stamina >= 10f)
                {
                    Light();
                    nextAttackTime = Time.time + 0.5f / attackRate;
                }
                if (Input.GetKeyDown(KeyCode.I) && stamina >= 20f)
                {
                    Medium();
                    nextAttackTime = Time.time + 0.5f / attackRate;
                }
                if (Input.GetKeyDown(KeyCode.J) && stamina >= 40f)
                {
                    Heavy();
                    nextAttackTime = Time.time + 0.5f / attackRate;
                }
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

    //player light attack
    void Light()
    {
        animator.SetTrigger("ATK_Light");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHit>().TakeDamage(DMG_light);
        }
        StartCoroutine(UseStamina(40f));
        pc.Freeze();
    }
    //player medium attack
    void Medium()
    {
        animator.SetTrigger("ATK_Medium");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHit>().TakeDamage(DMG_medium);
        }
        StartCoroutine(UseStamina(40f));
        pc.Freeze();
    }
    //player heavy attack which also is enabled to move certain objects
    void Heavy()
    {
        animator.SetTrigger("ATK_Heavy");
        StartCoroutine(Damage(DMG_heavy));
        StartCoroutine("moveObject");
        StartCoroutine(UseStamina(40f));
        pc.Freeze();
    }
    //allows the player to move certain objects with a heavy attack
    private IEnumerator moveObject()
    {
        Collider2D[] hitMovables = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, movableLayers);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D movable in hitMovables)
        {
            movable.GetComponent<Rigidbody2D>().AddForce(transform.up * 500000f);
            movable.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000000f);
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
}
