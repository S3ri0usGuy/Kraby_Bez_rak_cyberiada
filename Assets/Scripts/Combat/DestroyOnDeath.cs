using UnityEngine;

[RequireComponent(typeof(Damagable))]
public class DestroyOnDeath : MonoBehaviour
{
    private Damagable _damagable;

    [SerializeField, Min(0f)]
    private float delay = 0f;

    private void Awake()
    {
        _damagable = GetComponent<Damagable>();
        _damagable.Died += OnDied;
    }

    private void OnDied(Damagable damagable)
    {
        Destroy(gameObject, delay);
    }
}
