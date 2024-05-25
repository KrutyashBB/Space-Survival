using System;

namespace SpaceSurvival;

public class Ship : Unit
{
    private readonly ProgressBar _healthBar;
    private readonly FireTrail _fireTrail;
    public Rectangle Rect { get; private set; }
    private const float RotationSpeed = 3f;
    private const int BaseSpeed = 350;
    private const int AccelerationSpeed = 500;
    private Vector2 _minPos, _maxPos;
    private int _currentSpeed;

    private const float ShotSpeed = 0.5f;
    private float _shotTimeLeft;

    public readonly SoundEffectInstance EngineSound;
    private readonly SoundEffectInstance _shotSound;

    public Ship(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Scale = scale;
        _currentSpeed = BaseSpeed;

        EngineSound = Globals.Content.Load<SoundEffect>("Audio/engineSound").CreateInstance();
        _shotSound = Globals.Content.Load<SoundEffect>("Audio/shotSound").CreateInstance();
        _shotSound.Volume = 0.2f;

        CurrentHealth = MaxHealth = 100;
        Damage = 5;

        _shotTimeLeft = ShotSpeed;

        _healthBar = new ProgressBar(MaxHealth, Position, 0.2f);
        _fireTrail = new FireTrail(Globals.Content.Load<Texture2D>("fireTrail"), 1);
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
            Speed = 650
        };
        ProjectileManager.AddPlayerProjectile(pd);
    }

    public void SetBounds(Point mapSize)
    {
        _minPos = new Vector2(Origin.X, Origin.Y);
        _maxPos = new Vector2(mapSize.X - Origin.X, mapSize.Y - Origin.Y);
    }

    public void RestartGame()
    {
        CurrentHealth = MaxHealth;
    }

    public void Update()
    {
        if (InputManager.Direction == Vector2.Zero)
            EngineSound.Pause();
        else
            EngineSound.Resume();

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X / 2f, Position.Y - Size.Y * 0.9f));


        var minSize = Math.Min(Size.X, Size.Y);
        Rect = new Rectangle((int)Position.X - minSize / 2, (int)Position.Y - minSize / 2, minSize, minSize);

        if (InputManager.KeyboardState.IsKeyDown(Keys.LeftShift) && !InputManager.KeyboardState.IsKeyDown(Keys.Down))
        {
            _currentSpeed = AccelerationSpeed;
            _fireTrail.Update(new Vector2(Position.X, Position.Y), Rotation, true);
        }
        else
        {
            _currentSpeed = BaseSpeed;
            _fireTrail.Update(new Vector2(Position.X, Position.Y), Rotation, false);
        }

        Rotation += InputManager.Direction.X * RotationSpeed * Globals.TotalSeconds;
        Vector2 direction = new((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation));
        Position += InputManager.Direction.Y * direction * _currentSpeed * Globals.TotalSeconds;
        Position = Vector2.Clamp(Position, _minPos, _maxPos);

        _shotTimeLeft -= Globals.TotalSeconds;
        if (InputManager.KeyboardKeyPressed(Keys.Space))
        {
            if (_shotTimeLeft <= 0)
            {
                _shotSound.Play();
                Fire();
                _shotTimeLeft = ShotSpeed;
            }
        }
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None,
            1f);
        if (InputManager.Direction != Vector2.Zero)
            _fireTrail.Draw(Rotation + 1.55f);
        _healthBar.Draw();
    }
}