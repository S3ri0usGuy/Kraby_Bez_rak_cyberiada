using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    private int _hp;

    [SerializeField, Min(1)]
    private int maxHp = 100;

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
    private float healthRegenerationRate = 1f; //punkty zycia na sekunde
    private float regenerationCooldown = 5f; // Czas po ktoryn regeneracja  sie aktywuje
    private void OnDeath
    (
        //wywolanie eksplozji
        Instantiate(deathExplosionPrefab, transform.position, Quaterion.identify);
        //dodatkowo mozna zniszczyc obiekt
        Destroy(ganeObject)
    )
    private void Update()
    {
        if (IsAlive && Time.time - lastDamegeTime  >= regenerationCooldown)
        {
            Heal((int)(healthRegenerationRate * Time.deltaTime));
        }
    } 

    public void Heal(int HealValue)
    {
        if(!IsAlive) return;

        _hp = Math.Min (_hp + healValue, maxHp)
        CallHpChangedEvent();
    }

    private void OnEnable()
    {
        Hp = maxHp;
    }

    private void CallHpChangedEvent()
    {
        HpChanged?.Invoke(this);
    }

    public void InflictDamage(int damageValue)
    {
        if (!IsAlive) return;

        if (damageValue < 0)
            throw new ArgumentOutOfRangeException(nameof(damageValue));

        _hp -= damageValue;
        Damaged?.Invoke(this, damageValue);
        if (_hp <= 0)
        {
            Died?.Invoke(this);
        }

        CallHpChangedEvent();
    }

    public void InflictDamage(int damageValue, DamageTyoe damageTyoe)
    {
        if (!IsAlive) return;
        // tu mozna dodac fizyke obrazen jezeli ktos ma pomysk to zostawiam otwarta sciezke

        _hp -= damageValue
        Danaged?.Invoke(this, damageValue);
        if ( _hp <= 0)
        {
            Died?.Invoke(this):
        }
    }
    
}

public enum DamageTyoe
{
    Physical,
    Shot
}
