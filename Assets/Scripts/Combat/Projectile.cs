using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField, Min(0f)]
    private float speed;
    [SerializeField, Min(1)]
    private int damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.detectCollisions = false;
    }

    private void FixedUpdate()
    {
        _rigidbody.position += transform.forward * (speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damagable damagable))
        {
            damagable.InflictDamage(damage);
            Destroy(gameObject);
        }
    }
}
