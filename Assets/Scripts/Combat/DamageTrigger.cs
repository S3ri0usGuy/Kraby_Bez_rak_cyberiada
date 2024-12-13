using UnityEngine;

/// <summary>
/// This crap hits everything that collides with it
/// </summary>
public class DamageTrigger : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.InflictDamage(damage, DamageType.Physical);
        }
    }
}
