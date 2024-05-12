using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class PatrolMovementEnemyShip : MovementEnemyShips
{
    private readonly List<Vector2> _path = new();
    public static Point Range;
    private int _current;

    private readonly Random _random = new();

    public PatrolMovementEnemyShip(int countPoints)
    {
        GenerateWay(countPoints);
    }

    private void GenerateWay(int count)
    {
        for (var i = 0; i < count; i++)
        {
            float x = _random.Next(Range.X);
            float y = _random.Next(Range.Y);
            _path.Add(new Vector2(x, y));
        }
    }

    public override void Move(Sprite enemyShip)
    {
        if (_path.Count < 0) return;

        var dir = _path[_current] - enemyShip.Position;

        var rotation = (float)Math.Atan2(dir.Y, dir.X);
        rotation = MathHelper.ToDegrees(rotation);
        rotation += 90;
        rotation = MathHelper.ToRadians(rotation);
        enemyShip.Rotation = rotation;

        if (dir.Length() > 4)
        {
            dir.Normalize();
            enemyShip.Position += dir * enemyShip.Speed * Globals.TotalSeconds;
        }
        else
            _current = (_current + 1) % _path.Count;
    }
}