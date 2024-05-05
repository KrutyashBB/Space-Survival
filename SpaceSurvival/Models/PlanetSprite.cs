﻿using System;

namespace SpaceSurvival;

public class PlanetSprite : Sprite
{
    public int Id { get; private set; }
    public Rectangle Rect { get; private set; }

    private float _rotation;
    private const float RotationSpeed = 0.3f;
    private readonly float _randomSpeed;

    public bool IsCollision = false;
    private readonly SpriteFont _font;

    public PlanetSprite(int id, Texture2D tex, Vector2 pos, int scale) : base(tex, pos, scale)
    {
        Id = id;
        Scale = scale;

        var random = new Random();
        _randomSpeed = (float)random.NextDouble();

        Rect = new Rectangle((int)pos.X - Size.X / 2, (int)pos.Y - Size.Y / 2, Size.X, Size.Y);

        _font = Globals.Content.Load<SpriteFont>("font");
    }

    public void Update()
    {
        _rotation += _randomSpeed * RotationSpeed * Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None,
            1f);
        if (IsCollision)
            Globals.SpriteBatch.DrawString(_font, "Click TAB",
                new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y * 0.7f), Color.White);
    }
}