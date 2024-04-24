using System;

namespace SpaceSurvival;

public class HeroForMapGenerator : Sprite
{
    private Vector2 _coords;
    private Vector2 _direction;

    private float _movementTimer;
    private const float MovementDelay = 0.3f; //

    public HeroForMapGenerator(Texture2D tex, Vector2 coords, float scale) : base(tex, coords, scale)
    {
        _coords = coords;
        Position = new Vector2(coords.X * tex.Width * scale, coords.Y * tex.Width * scale);
    }

    public void Update()
    {
        _movementTimer += Globals.TotalSeconds;

        _direction = Vector2.Zero;
        if (_movementTimer > MovementDelay)
        {
            _direction = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
            _movementTimer = 0;
        }

        var newCoords = new Vector2(_coords.X + _direction.X, _coords.Y + _direction.Y);
        var newPos = new Vector2(newCoords.X * Texture.Width * Scale, _coords.Y * Texture.Height * Scale);
        if (MapGenerate.Map.IsWalkable((int)newCoords.X, (int)newCoords.Y))
        {
            _coords = newCoords;
            Position = Vector2.Lerp(Position, newPos, 0.1f);
        }
    }
}