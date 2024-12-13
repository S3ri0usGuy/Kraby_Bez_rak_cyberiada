using System;
using System.Collections;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    private int _hp;

    [SerializeField, Min(1)]
    public int maxHp = 100;

    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            CallHpChangedEvent();
        }
    }

    public bool IsAlive => _hp > 0;

    public event Action<Damagable> HpChanged;
    public event Action<Damagable, int> Damaged;
    public event Action<Damagable> Died;

    [SerializeField]
    private int healthRegenerationRate; //punkty zycia na sekunde
    [SerializeField]
    private float regenerationCooldown = 5f; // Czas po ktoryn regeneracja  sie aktywuje

    private void OnEnable()
    {
        Hp = maxHp;
        if (healthRegenerationRate > 0)
        {
            StartCoroutine(Regenerate());
        }
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenerationCooldown);
            Heal(healthRegenerationRate);
        }
    }

    public void Heal(int healValue)
    {
        if (!IsAlive) return;

        _hp = Math.Min(_hp + healValue, maxHp);
        CallHpChangedEvent();
    }

    private void CallHpChangedEvent()
    {
        HpChanged?.Invoke(this);
    }

    public void InflictDamage(int damageValue, DamageType damageType)
    {
        if (!IsAlive) return;

        if (damageValue < 0)
            throw new ArgumentOutOfRangeException(nameof(damageValue));

        // tu mozna dodac fizyke obrazen jezeli ktos ma pomysk to zostawiam otwarta sciezke

        _hp -= damageValue;
        Damaged?.Invoke(this, damageValue);
        if (_hp <= 0)
        {
            Died?.Invoke(this);
        }
        CallHpChangedEvent();
    }

}

public enum DamageType
{
    Physical,
    Shot
}
