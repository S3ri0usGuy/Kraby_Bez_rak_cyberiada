using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            controller.enabled = false;
            other.transform.position = target.position;
            other.transform.rotation = target.rotation;
            controller.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (target)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);

            Color blue = Color.blue;
            blue.a = 0.2f;
            Gizmos.color = blue;
            Gizmos.DrawSphere(target.position, 1f);
        }
    }
}