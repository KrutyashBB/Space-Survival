using System;

namespace SpaceSurvival;

public class EnemyShip : Unit
{
    public Rectangle Rect { get; private set; }

    public MovementEnemyShips MoveEnemy { get; set; }
    private readonly FollowMovementEnemyShip _followMovement;

    private double _timeSinceFire;
    private const double FireInterval = 0.5;


    public EnemyShip(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _followMovement = new FollowMovementEnemyShip();
        Speed = 150;

        Health = 30;
        Damage = 10;
    }

    private void Fire()
    {
        var pd = new ProjectileData
        {
            Type = ProjectileType.EnemyBullet,
            Position = Position,
            Rotation = Rotation,
            LifeSpan = 3f,
            Damage = Damage,
            Speed = 500
        };
        ProjectileManager.AddProjectile(pd);
    }

    private void ChangeMovementInRangePLayer(Ship ship)
    {
        var dir = ship.Position - Position;
        if (dir.Length() < 500 && MoveEnemy is PatrolMovementEnemyShip)
            MoveEnemy = _followMovement;
        else if (dir.Length() > 500 && MoveEnemy is FollowMovementEnemyShip)
            MoveEnemy = new PatrolMovementEnemyShip(10);
    }

    public void Update(Ship ship)
    {
        var minSize = Math.Min(Size.X, Size.Y);
        Rect = new Rectangle((int)Position.X - minSize / 2, (int)Position.Y - minSize / 2, minSize, minSize);

        ChangeMovementInRangePLayer(ship);
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