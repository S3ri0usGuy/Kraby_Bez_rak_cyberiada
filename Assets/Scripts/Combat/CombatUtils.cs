using UnityEngine;

public static class CombatUtils
{

    /// <summary>
    /// Shoots a "bullet" in the set direction.
    /// </summary>
    /// <param name="origin">A starting position of the bullet.</param>
    /// <param name="direction">Direction of the bullet.</param>
    /// <param name="shootingParams">Parameters of the bullet.</param>
    /// <param name="layerMask">Layer mask of the targets that will be hit.</param>
    /// <returns>
    /// An object that was damaged or <see langword="null" /> if missed or hit 
    /// non-damagable object.
    /// </returns>
    public static Damagable Shoot(Vector3 origin, Vector3 direction,
        ShootingParams shootingParams, LayerMask layerMask)
    {
        return Shoot(origin, direction, shootingParams, layerMask, origin);
    }

    /// <summary>
    /// Shoots a "bullet" in the set direction.
    /// </summary>
    /// <param name="origin">A starting position of the bullet.</param>
    /// <param name="direction">Direction of the bullet.</param>
    /// <param name="shootingParams">Parameters of the bullet.</param>
    /// <param name="layerMask">Layer mask of the targets that will be hit.</param>
    /// <param name="trailOrigin">First position of the bullet trail.</param>
    /// <returns>
    /// An object that was damaged or <see langword="null" /> if missed or hit 
    /// non-damagable object.
    /// </returns>
    public static Damagable Shoot(Vector3 origin, Vector3 direction,
        ShootingParams shootingParams, LayerMask layerMask,
        Vector3 trailOrigin)
    {
        Damagable damagable = null;

        // Apply spread
        float horizontalSpread = Random.Range(-shootingParams.horizontalSpread, shootingParams.horizontalSpread);
        float verticalSpread = Random.Range(-shootingParams.verticalSpread, shootingParams.verticalSpread);

        Quaternion rotation = Quaternion.Euler(verticalSpread, horizontalSpread, 0);

        // Rotate the direction vector
        Vector3 rotatedDirection = rotation * direction;

        Ray ray = new(origin, rotatedDirection);
        Vector3 trailEnd;

        // Cast a ray in the set direction
        if (Physics.Raycast(ray, out var hit, shootingParams.maxDistance,
            layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent(out damagable))
            {
                damagable.InflictDamage(shootingParams.damage, DamageType.Shot);
            }

            if (hit.collider.TryGetComponent(out ImpactSurface surface))
            {
                var vfxPrefab = surface.VfxPrefab;

                if (!vfxPrefab)
                {
                    Debug.LogWarning($"The f**king '{surface.name}' surface has no f**king VFX prefab.", surface);
                }
                else
                {
                    Object.Instantiate(vfxPrefab, hit.point, Quaternion.identity);
                }
            }

            trailEnd = hit.point;
        }
        else
        {
            trailEnd = origin + direction * shootingParams.maxDistance;
        }

        if (shootingParams.trailPrefab)
        {
            var trail = Object.Instantiate(shootingParams.trailPrefab);
            trail.Init(trailOrigin, trailEnd);
        }
        else
        {
            Debug.LogWarning("Where the f**k is the trail prefab?");
        }

        return damagable;
    }
}
