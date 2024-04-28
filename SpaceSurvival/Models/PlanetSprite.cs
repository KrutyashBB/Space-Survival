﻿using System;

namespace SpaceSurvival;

public class PlanetSprite : Sprite
{
    public TypePlanet Type;
    
    private float _rotation;
    private readonly float _rotationSpeed = 0.3f;
    private readonly float _randomSpeed;
    public Rectangle Rect { get; private set; }

    public bool IsCollision = false;
    private readonly SpriteFont _font;

    public PlanetSprite(Texture2D tex, Vector2 pos, TypePlanet type, int scale) : base(tex, pos, scale)
    {
        Type = type;
        
        var random = new Random();
        _randomSpeed = (float)random.NextDouble();
        
        Scale = scale;
        Rect = new Rectangle((int)(pos.X - Size.X / 5f), (int)(pos.Y - Size.Y / 4f), Size.X, Size.Y);

        _font = Globals.Content.Load<SpriteFont>("font");
    }

    public void Update()
    {
        _rotation += _randomSpeed * _rotationSpeed * Globals.TotalSeconds;
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