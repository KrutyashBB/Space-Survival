namespace SpaceSurvival;

public enum ProjectileType
{
    PlayerBullet,
    EnemyBullet
}
public class ProjectileData
{
    public ProjectileType Type { get; init; }
    public Vector2 Position { get; init; }
    public float Rotation { get; init; }
    public float LifeSpan { get; init; }
    public int Damage { get; init; }
    public int Speed { get; init; }
}