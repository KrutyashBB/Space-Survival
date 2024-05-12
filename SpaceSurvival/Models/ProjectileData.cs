namespace SpaceSurvival;

public enum ProjectileType
{
    PlayerBullet,
    EnemyBullet
}
public class ProjectileData
{
    public ProjectileType Type { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float LifeSpan { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }
}