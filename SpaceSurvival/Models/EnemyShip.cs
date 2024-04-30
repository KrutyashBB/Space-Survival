namespace SpaceSurvival;

public class EnemyShip : Sprite
{
    public MovementEnemyShips MoveEnemy { get; set; }

    public EnemyShip(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Speed = 150;
    }

    public void Update()
    {
        MoveEnemy.Move(this);
    }
}