using System;

[Serializable]
public struct ShootingParams
{
    /// <summary>
    /// Max shooting distance.
    /// </summary>
    public float maxDistance;
    /// <summary>
    /// Max horizontal (y axis) angle of a random bullets' direction change.
    /// </summary>
    public float horizontalSpread;
    /// <summary>
    /// Max horizontal (x axis) angle of a random bullets' direction change.
    /// </summary>
    public float verticalSpread;
    /// <summary>
    /// How much damage the bullet deals.
    /// </summary>
    public int damage;

    public float radius;

    /// <summary>
    /// Prefab of the bullet trail used.
    /// </summary>
    public BulletTrail trailPrefab;

    public ShootingParams(float maxDistance, float horizontalSpread,
        float verticalSpread, int damage, BulletTrail trailPrefab, float radius)
    {
        this.maxDistance = maxDistance;
        this.horizontalSpread = horizontalSpread;
        this.verticalSpread = verticalSpread;
        this.damage = damage;
        this.trailPrefab = trailPrefab;
        this.radius = radius;
    }

    // Builder pattern:

    public readonly ShootingParams MultiplySpread(float multiplier)
    {
        return new(maxDistance, horizontalSpread * multiplier,
            verticalSpread * multiplier, damage, trailPrefab, radius);
    }

    public readonly ShootingParams MultiplyDistance(float multiplier)
    {
        return new(maxDistance * multiplier, horizontalSpread,
            verticalSpread, damage, trailPrefab, radius);
    }

    public readonly ShootingParams MultiplyDamage(float multiplier)
    {
        return new(maxDistance, horizontalSpread, verticalSpread,
            (int)(damage * multiplier), trailPrefab, radius);
    }
}
