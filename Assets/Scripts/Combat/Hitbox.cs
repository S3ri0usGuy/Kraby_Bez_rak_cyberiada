using UnityEngine;

public class Hitbox : MonoBehaviour, IDamagable
{
    private Damagable _damagable;

    [SerializeField, Min(0f)]
    private float damageMultiplier = 1f;

    private void Awake()
    {
        _damagable = GetComponentInParent<Damagable>();
        if (!_damagable)
        {
            Debug.LogError("No Damagable in parent!", gameObject);
        }
    }

    public void InflictDamage(int damageValue, DamageType damageType)
    {
        _damagable.InflictDamage((int)(damageValue * damageMultiplier), damageType);
    }
}
