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
}