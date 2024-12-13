using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Damagable damagable; // Referencja do obiektu Damagable
    [SerializeField] private Image healthBarImage; // Referencja do komponentu Image paska zdrowia

    private void OnEnable()
    {
        if (damagable != null)
        {
            damagable.HpChanged += UpdateHealthBar;
            UpdateHealthBar(damagable);
        }
    }

    private void OnDisable()
    {
        if (damagable != null)
        {
            damagable.HpChanged -= UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(Damagable damagable)
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)damagable.Hp / damagable.MaxHp;
            UpdateHealthBarColor(damagable.Hp);
        }
    }

    private void UpdateHealthBarColor(int currentHp)
    {
        if (healthBarImage != null)
        {
            if (currentHp > damagable.MaxHp * 0.5f)
            {
                healthBarImage.color = Color.green; // Zielony
            }
            else if (currentHp > damagable.MaxHp * 0.2f)
            {
                healthBarImage.color = Color.yellow; // Żółty
            }
            else
            {
                healthBarImage.color = Color.red; // Czerwony
            }
        }
    }
}