using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    private int _hp;

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
}
