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

    public ShootingParams(float maxDistance, float horizontalSpread, float verticalSpread, int damage)
    {
        this.maxDistance = maxDistance;
        this.horizontalSpread = horizontalSpread;
        this.verticalSpread = verticalSpread;
        this.damage = damage;
    }

    // Builder pattern:

    public readonly ShootingParams MultiplySpread(float multiplier)
    {
        return new(maxDistance, horizontalSpread * multiplier, verticalSpread * multiplier, damage);
    }

    public readonly ShootingParams MultiplyDistance(float multiplier)
    {
        return new(maxDistance * multiplier, horizontalSpread, verticalSpread, damage);
    }

    public readonly ShootingParams MultiplyDamage(float multiplier)
    {
        return new(maxDistance, horizontalSpread, verticalSpread, (int)(damage * multiplier));
    }
}
