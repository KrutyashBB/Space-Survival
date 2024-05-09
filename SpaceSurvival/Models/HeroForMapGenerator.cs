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

    public HeroForMapGenerator(Texture2D tex, Vector2 coords, float scale, MapGenerate map) : base(tex, coords, scale)
    {
        _map = map;

        Coords = coords;
        Position = new Vector2(coords.X * tex.Width * scale, coords.Y * tex.Width * scale);
        UpdatePlayerFieldOfView();

        Health = 100;
        Damage = 3;
        Name = "Hero";
    }

    private void CheckCollisionWithEnemy(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            var dist = new Vector2(Math.Abs(Coords.X - enemy.Coords.X), Math.Abs(Coords.Y - enemy.Coords.Y));
            if (dist is { X: 0, Y: 1 } or { X: 1, Y: 0 })
            {
                enemy.TakeDamage(Damage);
                Health -= enemy.Damage;
                Console.WriteLine($"Player {Health} - Enemy {enemy.Health}");
            }
        }
    }

    private void UpdatePlayerFieldOfView()
    {
        _map.Map.ComputeFov((int)Coords.X, (int)Coords.Y, 5, true);
    }

    public void Update(List<Enemy> enemies)
    {
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
}