using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletTrail : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float duration = 0.2f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;

        Destroy(gameObject, duration);
    }

    public void Init(Vector3 from, Vector3 to, GameObject vfxPrefab)
    {
        _lineRenderer.SetPosition(0, from);
        _lineRenderer.SetPosition(1, to);

        if (vfxPrefab)
        {
            Instantiate(vfxPrefab, transform.parent);
        }
    }
}
