using UnityEngine;

public interface IDamagable
{
    
    void Damage(int damageAmount);

    void Die();

    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
}
