using System;

namespace SpaceSurvival;

public class Enemy : Unit
{
    public Vector2 Coords;
    private Vector2 _newPosition;

    private readonly IMap _map;
    private readonly PathToPlayer _path;

    private bool isAwareOfPlayer;

    private ProgressBar _healthBar;

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

        CurrentHealth = MaxHealth = 15;
        Damage = 5;
        Name = "Enemy";

        _healthBar = new ProgressBar(MaxHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f),
            0.2f);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Update(Vector2 playerCoords)
    {
        if (!_map.IsInFov((int)Coords.X, (int)Coords.Y))
            return;

        _movementTimer += Globals.TotalSeconds;

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f));

        if (_movementTimer > MovementDelay)
        {
            _path.CreateFromTO((int)Coords.X, (int)Coords.Y, (int)playerCoords.X, (int)playerCoords.Y);

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
        base.Draw();
        _healthBar.Draw();
    }
}