using System;

namespace SpaceSurvival;

public class Projectile : Sprite
{
    public Vector2 Direction { get; set; }
    public float LifeSpan { get; private set; }
    public Rectangle Rect { get; private set; }

    public Projectile(Texture2D tex, ProjectileData data, float scale) : base(tex, data.Position, scale)
    {
        Speed = data.Speed;
        Rotation = data.Rotation;
        Direction = new Vector2((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation));
        LifeSpan = data.LifeSpan;
    }

    public void Update()
    {
        Rect = new Rectangle((int)Position.X - Size.X / 2, (int)Position.Y - Size.Y / 2, Size.X, Size.Y);
        Position += Direction * Speed * Globals.TotalSeconds;
        LifeSpan -= Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None,
            1f);
    }
}