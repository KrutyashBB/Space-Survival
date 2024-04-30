namespace SpaceSurvival;

public class FollowMovementEnemyShip : MovementEnemyShips
{
    public Ship Target { get; set; }

    public override void Move(Sprite enemyShip)
    {
        if (Target is null) return;

        var dir = Target.Position - enemyShip.Position;

        if (dir.Length() > 4)
        {
            dir.Normalize();
            enemyShip.Position += dir * enemyShip.Speed * Globals.TotalSeconds;
        }
    }
}