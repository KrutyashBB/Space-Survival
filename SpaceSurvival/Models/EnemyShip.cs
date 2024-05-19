using System;

namespace SpaceSurvival;

public class EnemyShip : Unit
{
    public Rectangle Rect { get; private set; }
    private ProgressBar _healthBar;
        
    public MovementEnemyShips MoveEnemy { get; set; }
    private readonly FollowMovementEnemyShip _followMovement;

    private double _timeSinceFire;
    private const double FireInterval = 0.5;


    public EnemyShip(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _followMovement = new FollowMovementEnemyShip();
        Speed = 150;

        CurrentHealth = MaxHealth = 30;
        Damage = 10;

        _healthBar = new ProgressBar(MaxHealth, Position, 0.2f);
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
        ProjectileManager.AddEnemyProjectile(pd);
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
        
        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X / 2f, Position.Y - Size.Y * 0.9f));

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
        _healthBar.Draw();
    }
}