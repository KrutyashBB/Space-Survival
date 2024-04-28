
namespace SpaceSurvival;

public class Enemy : Sprite
{
    private Vector2 Coords;
    // private PathToPlayer Path;

    private PathToPlayer Path;

    private Vector2 _newPosition;

    private float _movementTimer;
    private const float MovementDelay = 0.8f;


    public Enemy(Texture2D tex, Vector2 pos, float scale, PathToPlayer path) : base(tex, pos, scale)
    {
        Path = path;
        Coords = pos;
        Position = new Vector2(Coords.X * tex.Width * scale, Coords.Y * tex.Height * scale);
        _newPosition = Position;
    }

    public void Update()
    {
        _movementTimer += Globals.TotalSeconds;

        if (_movementTimer > MovementDelay)
        {
            Path.CreateFrom((int)Coords.X, (int)Coords.Y);
            if (!Path.IsNearThePlayer)
            {
                var cell = Path.StepForward;
                if (cell != null)
                {
                    Coords.X = cell.X;
                    Coords.Y = cell.Y;
                    _newPosition = new Vector2(Coords.X * Texture.Width * Scale, Coords.Y * Texture.Height * Scale);
                }
            }

            _movementTimer = 0;
        }

        Position = Vector2.Lerp(Position, _newPosition, 0.1f);
    }

    public override void Draw()
    {
        // Path.Draw();
        base.Draw();
    }
}