// EnemyAttack.cs
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float attackDistance = 1.6f;
    public float attackCooldown = 1.5f;
    public WeaponDamage weapon;

    private Animator animator;
    private float timer;
    private bool attacking;

    private Health playerHealth;
    private EnemyAI enemyAI;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();

        if (player != null)
            playerHealth = player.GetComponent<Health>();

        if (weapon == null)
            Debug.LogError("❌ WEAPON no asignada en EnemyAttack", this);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (player == null || enemyAI == null || attacking) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (enemyAI.PlayerDetected &&
            distance <= attackDistance &&
            timer >= attackCooldown)
        {
            StartCoroutine(AttackRoutine());
            timer = 0f;
        }
    }

    IEnumerator AttackRoutine()
    {
        attacking = true;
        enemyAI.IsAttacking = true;
        enemyAI.agent.isStopped = true;

        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1.0f); // tiempo de ataque

        animator.SetBool("IsAttacking", false);
        enemyAI.agent.isStopped = false;

        attacking = false;
        enemyAI.IsAttacking = false;

        // Después de atacar, vuelve a patrulla si el jugador murió
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            enemyAI.ReturnToPatrol();
        }
    }

    public void EnableDamage()
    {
        if (weapon != null)
            weapon.EnableDamage();
    }

    public void DisableDamage()
    {
        if (weapon != null)
            weapon.DisableDamage();
    }
}
