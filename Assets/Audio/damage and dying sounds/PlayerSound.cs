using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private PitchShifter shifter;

    private void Start()
    {
        GetComponent<Damagable>().Damaged += OnDamaged;
    }

    private void OnDamaged(Damagable arg1, int arg2)
    {
        shifter.PlaySound();
    }
}
