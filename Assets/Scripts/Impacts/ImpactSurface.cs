using UnityEngine;

/// <summary>
/// A component that defines the bullet impact for the object.
/// </summary>
public class ImpactSurface : MonoBehaviour
{
    [SerializeField]
    private GameObject vfxPrefab;

    public GameObject VfxPrefab => vfxPrefab;
}
