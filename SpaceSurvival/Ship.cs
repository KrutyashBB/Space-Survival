using System;

namespace SpaceSurvival;

public class Ship : Sprite
{
    private float _rotation;
    private readonly float _rotationSpeed = 3f;

    public Ship(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
    }

    public void Update()
    {
        _rotation += InputManager.Direction.X * _rotationSpeed * Globals.TotalSeconds;
        Vector2 direction = new((float)Math.Sin(_rotation), -(float)Math.Cos(_rotation));
        Position += InputManager.Direction.Y * direction * Speed * Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 1);
    }
}