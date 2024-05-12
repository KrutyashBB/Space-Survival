using System;

namespace SpaceSurvival;

public class Ship : Unit
{
    private const float RotationSpeed = 3f;
    private const int BaseSpeed = 350;
    private const int AccelerationSpeed = 500;
    // private float _rotation;
    private Vector2 _minPos, _maxPos;
    private int _currentSpeed;
    public Rectangle Rect { get; private set; }

    public Ship(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Scale = scale;
        _currentSpeed = BaseSpeed;

        Health = 100;
        Damage = 5;
    }

    private void Fire()
    {
        var pd = new ProjectileData
        {
            Type = ProjectileType.PlayerBullet,
            Position = Position,
            Rotation = Rotation,
            LifeSpan = 3f,
            Damage = Damage,
            Speed = 600
        };
        ProjectileManager.AddProjectile(pd);
    }

    public void SetBounds(Point mapSize)
    {
        _minPos = new Vector2(Origin.X, Origin.Y);
        _maxPos = new Vector2(mapSize.X - Origin.X, mapSize.Y - Origin.Y);
    }

    public void Update()
    {
        var minSize = Math.Min(Size.X, Size.Y);
        Rect = new Rectangle((int)Position.X - minSize / 2, (int)Position.Y - minSize / 2, minSize, minSize);

        if (InputManager.KeyboardState.IsKeyDown(Keys.LeftShift) && !InputManager.KeyboardState.IsKeyDown(Keys.Down))
            _currentSpeed = AccelerationSpeed;
        else
            _currentSpeed = BaseSpeed;

        Rotation += InputManager.Direction.X * RotationSpeed * Globals.TotalSeconds;
        Vector2 direction = new((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation));
        Position += InputManager.Direction.Y * direction * _currentSpeed * Globals.TotalSeconds;
        Position = Vector2.Clamp(Position, _minPos, _maxPos);

        if (InputManager.KeyPressed(Keys.Space))
            Fire();
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None,
            1f);
    }
}