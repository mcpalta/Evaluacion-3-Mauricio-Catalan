using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool IsDead { get; protected set; }

    public bool destroyOnDeath = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public virtual void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDead = true;
        currentHealth = 0;

        Debug.Log(gameObject.name + " murió");

        Animator anim = GetComponent<Animator>();

        // 🔒 Protección: solo dispara el trigger si existe
        if (anim != null && anim.HasParameterOfType("Die", AnimatorControllerParameterType.Trigger))
        {
            anim.SetTrigger("Die");
        }

        if (destroyOnDeath)
        {
            Destroy(gameObject, 2f);
        }
    }
}

/// <summary>
/// Extensión segura para comprobar parámetros del Animator
/// </summary>
public static class AnimatorExtensions
{
    public static bool HasParameterOfType(this Animator animator, string name, AnimatorControllerParameterType type)
    {
        foreach (var param in animator.parameters)
        {
            if (param.name == name && param.type == type)
                return true;
        }
        return false;
    }
}
