namespace SpaceSurvival;

public class SpaceBackground : Sprite
{
    private const float Scale = 3f;
    public Point MapSize { get; private set; }

    public SpaceBackground(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        MapSize = new Point((int)(tex.Width * Scale), (int)(tex.Height * Scale));
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}