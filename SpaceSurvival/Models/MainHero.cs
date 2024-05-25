using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Color = Microsoft.Xna.Framework.Color;

namespace SpaceSurvival;

public enum StateAnimation
{
    LeftAttack,
    RightAttack
}

public class MainHero : Unit
{
    public MapGenerate Map { get; set; }
    private const int MapCellTexSize = 16;

    public Vector2 Coords { get; set; }
    private Vector2 _direction;
    private readonly AnimationManager _anims = new();
    private StateAnimation _stateAnimation;

    private const float MovementAnimationSpeed = 0.1f;

    private float _movementTimer;
    private const float MovementDelay = 0.4f;

    private float _attackTimer;
    private const float AttackDelay = 2f;

    private readonly ProgressBar _healthBar;

    private readonly SoundEffectInstance _swordHitSound;

    public MainHero(Texture2D tex, Vector2 coords, float scale, MapGenerate map) : base(tex, coords, scale)
    {
        Map = map;

        Coords = coords;
        Size = new Point((int)(MapCellTexSize * Scale), (int)(MapCellTexSize * Scale));
        Position = new Vector2(coords.X * MapCellTexSize * scale, coords.Y * MapCellTexSize * scale);
        UpdatePlayerFieldOfView();

        _anims.AddAnimation(new Vector2(0, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed));
        _anims.AddAnimation(StateAnimation.LeftAttack, new Animation(Texture, 8, 6, 0.21f, 3));
        _anims.AddAnimation(StateAnimation.RightAttack, new Animation(Texture, 8, 6, 0.21f, 4));
        _anims.AddAnimation(new Vector2(-1, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(-1, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(-1, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(1, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(0, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(0, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(1, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(1, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));

        CurrentHealth = MaxHealth = 20;
        Damage = 3;
        Name = "Hero";

        _healthBar = new ProgressBar(MaxHealth, Position, 0.2f);

        _swordHitSound = Globals.Content.Load<SoundEffect>("Audio/swordHitSound").CreateInstance();
    }

    private Enemy GetNearestEnemy(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            var dist = new Vector2(Coords.X - enemy.Coords.X, Math.Abs(Coords.Y - enemy.Coords.Y));
            if (dist == new Vector2(-1, 0) || dist == new Vector2(1, 0) || dist == new Vector2(0, 1))
            {
                if (dist == new Vector2(1, 0) || dist == new Vector2(0, 1))
                    _stateAnimation = StateAnimation.LeftAttack;
                else if (dist == new Vector2(-1, 0))
                    _stateAnimation = StateAnimation.RightAttack;
                return enemy;
            }
        }

        return null;
    }

    private void AttackEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        _swordHitSound.Play();
        enemy.TakeDamage(Damage);
        CurrentHealth -= enemy.Damage;
        Console.WriteLine($"Player {CurrentHealth} - Enemy {enemy.CurrentHealth}");
    }

    private void UpdatePlayerFieldOfView()
    {
        Map.Map.ComputeFov((int)Coords.X, (int)Coords.Y, 5, true);
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
    }

    public void Update(List<Enemy> enemies)
    {
        var nearestEnemy = GetNearestEnemy(enemies);

        if (nearestEnemy != null)
            _anims.Update(_stateAnimation);
        else
            _anims.Update(InputManager.Direction);

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f));

        _movementTimer += Globals.TotalSeconds;

        _direction = Vector2.Zero;
        if (_movementTimer > MovementDelay)
        {
            _direction = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
            UpdatePlayerFieldOfView();
            _movementTimer = 0;
        }

        _attackTimer += Globals.TotalSeconds;
        if (_attackTimer > AttackDelay)
        {
            AttackEnemy(nearestEnemy);
            _attackTimer = 0;
        }

        var newCoords = new Vector2(Coords.X + _direction.X, Coords.Y + _direction.Y);
        var newPos = new Vector2(newCoords.X * Size.X, newCoords.Y * Size.Y);
        var enemy = enemies.FirstOrDefault(x => x.Coords == newCoords);

        if (Map.Map.IsWalkable((int)newCoords.X, (int)newCoords.Y) && enemy == null)
        {
            Coords = newCoords;
            Position = Vector2.Lerp(Position, newPos, 0.1f);
        }
    }

    public override void Draw()
    {
        _anims.Draw(new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y), Rotation, Vector2.Zero);
        _healthBar.Draw();
    }
}