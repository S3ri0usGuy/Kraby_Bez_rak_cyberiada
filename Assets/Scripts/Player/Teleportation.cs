using UnityEngine;
using System;

public class TeleportOnTriger : MonoBehaviour
{
    [SerializeField]
    private void OnTigerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetPosition.position;
        }
    }
}