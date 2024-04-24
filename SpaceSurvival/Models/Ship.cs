using System;

namespace SpaceSurvival;

public class Ship : Sprite
{
    private float _rotation;
    private readonly float _rotationSpeed = 3f;
    private Vector2 _minPos, _maxPos;
    private int Speed = 300;
    public Rectangle Rect { get; private set; }

    public Ship(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Scale = scale;
    }

    public void SetBounds(Point mapSize)
    {
        _minPos = new Vector2(Origin.X, Origin.Y);
        _maxPos = new Vector2(mapSize.X - Origin.X, mapSize.Y - Origin.Y);
    }

    public void Update()
    {
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
        _rotation += InputManager.Direction.X * _rotationSpeed * Globals.TotalSeconds;
        Vector2 direction = new((float)Math.Sin(_rotation), -(float)Math.Cos(_rotation));
        Position += InputManager.Direction.Y * direction * Speed * Globals.TotalSeconds;
        Position = Vector2.Clamp(Position, _minPos, _maxPos);
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None,
            1f);
    }
}