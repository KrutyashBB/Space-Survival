using System;
using System.Collections.Generic;
using System.Linq;

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
    public bool IsAttacking { get; private set; }
    private float _attackTimer;
    private const float AttackDelay = 1f;

    private readonly ProgressBar _healthBar;
    public readonly SoundEffectInstance SwordBattleSound;

    public MainHero(Texture2D tex, Vector2 coords, float scale, MapGenerate map) : base(tex, coords, scale)
    {
        Map = map;
        Coords = coords;
        Size = new Point((int)(MapCellTexSize * Scale), (int)(MapCellTexSize * Scale));
        Position = new Vector2(coords.X * MapCellTexSize * scale, coords.Y * MapCellTexSize * scale);
        UpdatePlayerFieldOfView();

        _anims.AddAnimation(new Vector2(0, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed));
        _anims.AddAnimation(StateAnimation.LeftAttack, new Animation(Texture, 8, 6, 0.15f, 3));
        _anims.AddAnimation(StateAnimation.RightAttack, new Animation(Texture, 8, 6, 0.15f, 4));
        _anims.AddAnimation(new Vector2(-1, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(-1, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(-1, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 5));
        _anims.AddAnimation(new Vector2(1, 0), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(0, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(0, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(1, -1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));
        _anims.AddAnimation(new Vector2(1, 1), new Animation(Texture, 8, 6, MovementAnimationSpeed, 6));

        CurrentHealth = MaxHealth = 50;
        Damage = 3;

        _healthBar = new ProgressBar(MaxHealth, Position, 0.2f);
        SwordBattleSound = Globals.Content.Load<SoundEffect>("Audio/swordBattleSound").CreateInstance();
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

    private void Battle(Enemy enemy)
    {
        if (enemy == null)
        {
            IsAttacking = false;
            SwordBattleSound.Stop();
            return;
        }

        IsAttacking = true;
        SwordBattleSound.Play();
        enemy.TakeDamage(Damage);
        CurrentHealth -= enemy.Damage;
    }

    private void UpdatePlayerFieldOfView() =>
        Map.Map.ComputeFov((int)Coords.X, (int)Coords.Y, 5, true);

    public void Reset()
    {
        SwordBattleSound.Stop();
        CurrentHealth = MaxHealth;
    }

    private void UpdateAnimation(Enemy enemy)
    {
        if (enemy != null)
            _anims.Update(_stateAnimation);
        else
            _anims.Update(InputManager.Direction);
    }

    public void Update(List<Enemy> enemies)
    {
        var nearestEnemy = GetNearestEnemy(enemies);
        UpdateAnimation(nearestEnemy);

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f));

        _attackTimer += Globals.TotalSeconds;
        if (_attackTimer > AttackDelay)
        {
            Battle(nearestEnemy);
            _attackTimer = 0;
        }

        _direction = Vector2.Zero;
        _movementTimer += Globals.TotalSeconds;
        if (_movementTimer > MovementDelay)
        {
            _direction = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
            UpdatePlayerFieldOfView();
            _movementTimer = 0;
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