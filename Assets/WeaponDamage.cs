using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 10;
    private bool canDamage;

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        Health health = other.GetComponent<Health>();
        if (health != null && !health.IsDead)
        {
            health.TakeDamage(damage);
        }
    }
}
