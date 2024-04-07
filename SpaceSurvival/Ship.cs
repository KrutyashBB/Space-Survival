using System;

namespace SpaceSurvival;

public class Ship : Sprite
{
    private float _rotation;
    private readonly float _rotationSpeed = 3f;
    private Vector2 _minPos, _maxPos;

    public Ship(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
    }

    public void SetBounds(Point mapSize)
    {
        _minPos = new Vector2(Origin.X, Origin.Y);
        _maxPos = new Vector2(mapSize.X - Origin.X, mapSize.Y - Origin.Y);
    }

    public void Update()
    {
        _rotation += InputManager.Direction.X * _rotationSpeed * Globals.TotalSeconds;
        Vector2 direction = new((float)Math.Sin(_rotation), -(float)Math.Cos(_rotation));
        Position += InputManager.Direction.Y * direction * Speed * Globals.TotalSeconds;
        Position = Vector2.Clamp(Position, _minPos, _maxPos);
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 1f);
    }
}