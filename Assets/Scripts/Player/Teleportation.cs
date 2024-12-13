using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // Obiekt, do którego przeniesiemy postać

    private void OnTriggerEnter(Collider other) // Poprawiona nazwa metody
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetPosition.position; // Przenieś postać do nowej pozycji
        }
    }
}