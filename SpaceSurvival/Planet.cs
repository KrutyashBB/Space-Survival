using System;

namespace SpaceSurvival;

public class Planet : Sprite
{
    private float _rotation;
    private readonly float _rotationSpeed = 0.3f;
    private readonly float _randomSpeed;

    public Planet(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        var random = new Random();
        _randomSpeed = (float)random.NextDouble();
    }

    public void Update()
    {
        _rotation += _randomSpeed * _rotationSpeed * Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 1f);
    }
}