using UnityEngine;
using system;

public class Granade : MonoBehaviour
{
    [SerializeField]
    private float throwForce = 10f; //sila rzutu granatem
    [SerializeField]
    private float explosionDelay = 3f ;//czas do wybuchu
    [SerializeField]
    private float explosionRadious  = 5f ;//promien wybuchu
    [SerializeField]
    private int damage  = 50 ;// zadawane obrazenia
    [SerializeField]
    private GameObject explosionEffect; // Prefab efektu wybuchu


    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent <Rigidbody>();
    }

    public void Throw(Vector3 direction)
    {
        rb.AddForce(direction * throwForce, ForceMode.Impulse, VelocityChange);
        StartCorutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        vield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    private void Explode()
    {
        //wywolanie efektu eksplozji
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        //zniszczenie granatu po wybuchu
        Destroy(gameObject)
    }

    private void OnDrawGizmosSelected()
    {
        //wizualizacja promienia wybuchu granatu
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}