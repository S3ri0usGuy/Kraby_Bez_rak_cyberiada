using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float throwForce = 10f; //sila rzutu granatem
    [SerializeField]
    private float explosionDelay = 3f;//czas do wybuchu
    [SerializeField]
    private float explosionRadius = 5f;//promien wybuchu
    [SerializeField]
    private int damage = 100;// zadawane obrazenia
    [SerializeField]
    private int damageToPlayer = 50;
    [SerializeField]
    private GameObject explosionEffect; // Prefab efektu wybuchu

    [SerializeField]
    private LayerMask explosionLayers = -1;

    [SerializeField]
    private float angular = 90f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction)
    {
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
        rb.AddTorque(Random.onUnitSphere * Random.Range(0f, angular), ForceMode.Impulse);
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    private void Explode()
    {
        //wywolanie efektu eksplozji
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        var colliders = Physics.OverlapSphere(transform.position, explosionRadius,
            explosionLayers, QueryTriggerInteraction.Collide);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamagable damagable))
            {
                bool isPlayer = collider.gameObject.CompareTag("Player");
                damagable.InflictDamage(isPlayer ? damageToPlayer : damage, DamageType.Shot);
            }
        }

        //zniszczenie granatu po wybuchu
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        //wizualizacja promienia wybuchu granatu
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}