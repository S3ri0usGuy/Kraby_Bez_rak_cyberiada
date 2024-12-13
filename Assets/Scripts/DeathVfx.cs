using UnityEngine;

public class DeathVfx : MonoBehaviour
{
    private Damagable _damagable;

    [SerializeField]
    private GameObject vfxPrefab;

    private void Awake()
    {
        _damagable = GetComponentInParent<Damagable>();
        if (!_damagable)
        {
            Debug.LogError("No Damagable in parent!", gameObject);
        }
    }

    private void OnEnable()
    {
        _damagable.Died += OnDied;
    }

    private void OnDisable()
    {
        _damagable.Died -= OnDied;
    }

    private void OnDied(Damagable damagable)
    {
        Instantiate(vfxPrefab, transform.position, transform.rotation);
    }
}
