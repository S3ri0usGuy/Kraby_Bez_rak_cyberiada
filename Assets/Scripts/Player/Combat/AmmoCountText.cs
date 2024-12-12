using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class AmmoCountText : MonoBehaviour
{
    private TMP_Text _text;

    [SerializeField]
    private PlayerCombat combat;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        combat.BulletsChanged += OnBulletsUpdated;
        UpdateText();
    }

    private void OnDisable()
    {
        combat.BulletsChanged -= OnBulletsUpdated;
    }

    private void UpdateText()
    {
        _text.text = $"{combat.BulletsInMagazine}/{combat.MagazineCapacity}";
    }

    private void OnBulletsUpdated(PlayerCombat obj)
    {
        UpdateText();
    }
}
