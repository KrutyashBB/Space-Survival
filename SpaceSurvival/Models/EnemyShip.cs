namespace SpaceSurvival;

public class EnemyShip : Sprite
{
    public MovementEnemyShips MoveEnemy { get; set; }

    private double _timeSinceFire;
    private const double FireInterval = 0.25; // Interval between shots
    
    private readonly FollowMovementEnemyShip _followMovement;

    public EnemyShip(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _followMovement = new FollowMovementEnemyShip();
        Speed = 150;
    }

    public void Fire()
    {
        var pd = new ProjectileData
        {
            Position = Position,
            Rotation = Rotation,
            LifeSpan = 5,
            Speed = 500
        };
        ProjectileManager.AddProjectile(pd);
    }

    public void Update(Ship ship)
    {
        var dir = ship.Position - Position;
        if (dir.Length() < 500 && MoveEnemy is PatrolMovementEnemyShip)
            MoveEnemy = _followMovement;
        else if (dir.Length() > 500 && MoveEnemy is FollowMovementEnemyShip)
            MoveEnemy = new PatrolMovementEnemyShip(10);
        
        MoveEnemy.Move(this);
        if (MoveEnemy is FollowMovementEnemyShip)
        {
            _timeSinceFire += Globals.TotalSeconds;
            if (_timeSinceFire >= FireInterval)
            {
                Fire();
                _timeSinceFire = 0;
            }
        }
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None,
            1f);
    }
}