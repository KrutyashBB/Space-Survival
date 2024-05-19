using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class EnemyShipManager
{
    public List<EnemyShip> EnemyShips { get; } = new();
    private int _countEnemies;
    private Texture2D _texture = Globals.Content.Load<Texture2D>("tiny_ship8");
    private static Random _random;
    private static int _padding;


    public EnemyShipManager(int countEnemies, Point sizeWindow)
    {
        _random = new Random();
        _padding = _texture.Width / 2;
        _countEnemies = countEnemies;
        AddEnemyShips(sizeWindow);
    }


    private static Vector2 RandomPosition(Point size)
    {
        var w = size.X;
        var h = size.Y;
        var pos = new Vector2();

        if (_random.NextDouble() < w / (w + h))
        {
            pos.X = (int)(_random.NextDouble() * w);
            pos.Y = _random.NextDouble() < 0.5 ? -_padding : h + _padding;
        }
        else
        {
            pos.Y = (int)(_random.NextDouble() * h);
            pos.X = _random.NextDouble() < 0.5 ? -_padding : w + _padding;
        }

        return pos;
    }

    private void AddEnemyShips(Point size)
    {
        for (var i = 0; i < _countEnemies; i++)
            EnemyShips.Add(new EnemyShip(_texture, RandomPosition(size), 1f)
                { MoveEnemy = new PatrolMovementEnemyShip(10) });
    }

    public void Update(Ship ship)
    {
        EnemyShips.RemoveAll(enemyShip => enemyShip.CurrentHealth <= 0);
        foreach (var enemyShip in EnemyShips)
            enemyShip.Update(ship);
    }

    public void Draw()
    {
        foreach (var enemyShip in EnemyShips)
            enemyShip.Draw();
    }
}