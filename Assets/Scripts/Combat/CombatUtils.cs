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
        Damagable damagable = null;

        // Apply spread
        float horizontalSpread = Random.Range(-shootingParams.horizontalSpread, shootingParams.horizontalSpread);
        float verticalSpread = Random.Range(-shootingParams.verticalSpread, shootingParams.verticalSpread);

        Quaternion rotation = Quaternion.Euler(verticalSpread, horizontalSpread, 0);

        // Rotate the direction vector
        Vector3 rotatedDirection = rotation * direction;

        Ray ray = new(origin, rotatedDirection);
        // Cast a ray in the set direction
        if (Physics.Raycast(ray, out var hit, shootingParams.maxDistance,
            layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent(out damagable))
            {
                damagable.InflictDamage(shootingParams.damage, DamageType.Shot);
            }

            // Visualize this shit here
        }

        return damagable;
    }
}
