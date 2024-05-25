using System;

namespace SpaceSurvival;

public class FollowMovementEnemyShip : MovementEnemyShips
{
    public static Ship Target { get; set; }

    public override void Move(Sprite enemyShip)
    {
        if (Target is null) return;

        var dir = Target.Position - enemyShip.Position;

        var rotation = (float)Math.Atan2(dir.Y, dir.X);
        rotation = MathHelper.ToDegrees(rotation);
        rotation += 90;
        rotation = MathHelper.ToRadians(rotation);
        enemyShip.Rotation = rotation;

        if (dir.Length() > 100)
        {
            dir.Normalize();
            enemyShip.Position += dir * enemyShip.Speed * Globals.TotalSeconds;
        }
    }
}