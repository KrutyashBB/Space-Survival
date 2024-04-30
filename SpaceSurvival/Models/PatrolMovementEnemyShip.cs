using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class PatrolMovementEnemyShip : MovementEnemyShips
{
    private readonly List<Vector2> _path = new();
    private int _current;

    private readonly Random _random = new();

    public PatrolMovementEnemyShip(int countPoints, Point range)
    {
        GenerateWay(countPoints, range);
    }

    public void AddWayPoint(Vector2 point)
    {
        _path.Add(point);
    }

    public void GenerateWay(int count, Point range)
    {
        // _path.Clear();

        for (var i = 0; i < count; i++)
        {
            float x = _random.Next(range.X);
            float y = _random.Next(range.Y);
            _path.Add(new Vector2(x, y));
        }
    }

    public override void Move(Sprite enemyShip)
    {
        if (_path.Count < 0) return;

        var dir = _path[_current] - enemyShip.Position;
        if (dir.Length() > 4)
        {
            dir.Normalize();
            enemyShip.Position += dir * enemyShip.Speed * Globals.TotalSeconds;
        }
        else
            _current = (_current + 1) % _path.Count;
    }
}