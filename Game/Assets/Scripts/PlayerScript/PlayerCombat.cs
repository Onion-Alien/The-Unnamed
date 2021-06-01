using System.Collections;
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

    public int dmgLight = 20;
    public int dmgHeavy = 40;
    public float attackRange = 0.5f;
    public bool canInteract;

    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;

    public float stamina = 100f;
    public float maxStamina = 100f;
    private float staminaRegenTimer = 1f;
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
        stamBar.showHP((int)stamina, (int)maxStamina);
    }

    private void Update()
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
            if (staminaRegenTimer >= StaminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (StaminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina); //sets stamina based on delta time
                stamBar.Set(Mathf.RoundToInt(stamina)); //rounds to int because hp bar needs floats
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
            }
        }
    }
    
    public void Interact(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            Collider2D[] portals = Physics2D.OverlapCircleAll(transform.position, 5f);
            foreach (var x in portals)
            {
                Portal portal = x.GetComponent<Portal>();
                if (portal != null)
                {
                    portal.Interact();
                }
            }
        }
    }

    public void increaseStam(float value)
    {
        if ((stamina + value) <= maxStamina)
        {
            stamina += value;
            stamBar.Set((int)stamina);
        }
        else
        {
            stamina = maxStamina;
            stamBar.Set((int)maxStamina);

        }
        stamBar.showHP((int)stamina, (int)maxStamina);
    }
    
    public IEnumerator UseStamina(float stamCost)
    {
        yield return new WaitForSeconds(0.2f);
        stamina -= stamCost;
        stamBar.Set(Mathf.RoundToInt(stamina));
        staminaRegenTimer = 0.0f;
        stamBar.showHP((int)stamina, (int)maxStamina);
    }

    private IEnumerator SetStamina(float stam)
    {
        yield return new WaitForSeconds(0.2f);
        stamina += stam;
        stamBar.Set(Mathf.RoundToInt(stamina));
        staminaRegenTimer = 0.0f;
    }

    //player light attack
    public void Light(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime && stamina >= 40 && !pc.isBlocking)
        {
            animator.SetTrigger("ATK_Light");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHit>().TakeDamage(dmgLight);
            }
            StartCoroutine(UseStamina(20f));
            pc.Freeze();
            nextAttackTime = Time.time + 0.5f / attackRate;
        }
    }

    //player heavy attack which also is enabled to move certain objects
    public void Heavy(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime && stamina >= 40 && !pc.isBlocking)
        {
            animator.SetTrigger("ATK_Heavy");
            StartCoroutine(Damage(dmgHeavy));
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
        switch (attack)
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
                        movable.GetComponent<Rigidbody2D>().AddForce(transform.right * -1000000f);
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
            if (enemy.GetComponent<BOSS_Arm_Smasher>())
            {
                enemy.GetComponent<BOSS_Arm_Smasher>().TakeDamage(dmg);
                foreach (SpriteRenderer x in enemy.GetComponentsInChildren<SpriteRenderer>())
                {
                    x.GetComponent<SpriteRenderer>().color = Color.red;
                }
                yield return new WaitForSeconds(0.1f);
                foreach (SpriteRenderer x in enemy.GetComponentsInChildren<SpriteRenderer>())
                {
                    x.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            else
            {
                enemy.GetComponent<EnemyHit>().TakeDamage(dmg);
            }
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

    public void setAttackRate(float rate)
    {
        attackRate = rate;
    }

    public void setStamina(float stamina)
    {
        maxStamina = stamina;
    }

    public void setStaminaRegen(float stamRegen)
    {
        staminaRegenTimer = stamRegen;
    }
    
    public float getAttackRate()
    {
        return attackRate;
    }

    public float getStamina()
    {
        return maxStamina;
    }

    public float getStaminaRegen()
    {
        return staminaRegenTimer;
    }
}
