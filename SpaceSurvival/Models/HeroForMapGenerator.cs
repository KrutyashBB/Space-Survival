using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceSurvival;

public class HeroForMapGenerator : Unit
{
    private readonly MapGenerate _map;
    public Vector2 Coords { get; private set; }
    private Vector2 _direction;

    private float _movementTimer;
    private const float MovementDelay = 0.3f;

    private readonly ProgressBar _healthBar;

    public HeroForMapGenerator(Texture2D tex, Vector2 coords, float scale, MapGenerate map) : base(tex, coords, scale)
    {
        _map = map;

        Coords = coords;
        Position = new Vector2(coords.X * tex.Width * scale, coords.Y * tex.Width * scale);
        UpdatePlayerFieldOfView();

        CurrentHealth = MaxHealth = 100;
        Damage = 3;
        Name = "Hero";

        _healthBar = new ProgressBar(MaxHealth, Position, 0.2f);
    }

    private void CheckCollisionWithEnemy(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            var dist = new Vector2(Math.Abs(Coords.X - enemy.Coords.X), Math.Abs(Coords.Y - enemy.Coords.Y));
            if (dist is { X: 0, Y: 1 } or { X: 1, Y: 0 })
            {
                enemy.TakeDamage(Damage);
                CurrentHealth -= enemy.Damage;
                Console.WriteLine($"Player {CurrentHealth} - Enemy {enemy.CurrentHealth}");
            }
        }
    }

    private void UpdatePlayerFieldOfView()
    {
        _map.Map.ComputeFov((int)Coords.X, (int)Coords.Y, 5, true);
    }

    public void Update(List<Enemy> enemies)
    {
        if (CurrentHealth < 0)
        {
            SceneManager.SwitchScene((int)TypeScene.PlayerDeathScene);
            CurrentHealth = MaxHealth;
        }

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f));

        _movementTimer += Globals.TotalSeconds;

        _direction = Vector2.Zero;
        if (_movementTimer > MovementDelay)
        {
            _direction = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
            CheckCollisionWithEnemy(enemies);
            UpdatePlayerFieldOfView();
            _movementTimer = 0;
        }

        var newCoords = new Vector2(Coords.X + _direction.X, Coords.Y + _direction.Y);
        var newPos = new Vector2(newCoords.X * Texture.Width * Scale, newCoords.Y * Texture.Height * Scale);
        var enemy = enemies.FirstOrDefault(x => x.Coords == newCoords);

        if (_map.Map.IsWalkable((int)newCoords.X, (int)newCoords.Y) && enemy == null)
        {
            Coords = newCoords;
            Position = Vector2.Lerp(Position, newPos, 0.1f);
        }
    }

    public override void Draw()
    {
        base.Draw();
        _healthBar.Draw();
    }
}