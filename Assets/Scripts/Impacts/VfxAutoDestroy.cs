using UnityEngine;
using UnityEngine.VFX;

public class VfxAutoDestroy : MonoBehaviour
{
    private VisualEffect _vfx;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (_vfx.aliveParticleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
