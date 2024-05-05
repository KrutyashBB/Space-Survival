using System;

namespace SpaceSurvival;

public class Enemy : Unit
{
    public Vector2 Coords;
    private Vector2 _newPosition;

    private readonly IMap _map;
    private readonly PathToPlayer _path;

    private bool isAwareOfPlayer;

    private float _movementTimer;
    private const float MovementDelay = 0.8f;


    public Enemy(Texture2D tex, Vector2 pos, float scale, PathToPlayer path, IMap map) :
        base(tex, pos, scale)
    {
        _path = path;
        _map = map;

        Coords = pos;
        Position = new Vector2(Coords.X * tex.Width * scale, Coords.Y * tex.Height * scale);
        _newPosition = Position;

        Health = 15;
        Damage = 5;
        Name = "Enemy";
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Update()
    {
        if (!_map.IsInFov((int)Coords.X, (int)Coords.Y))
            return;

        _movementTimer += Globals.TotalSeconds;

        if (_movementTimer > MovementDelay)
        {
            _path.CreateFrom((int)Coords.X, (int)Coords.Y);

            if (!_path.IsNearThePlayer)
            {
                var cell = _path.StepForward;
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
        // _path.Draw();
        base.Draw();
    }
}