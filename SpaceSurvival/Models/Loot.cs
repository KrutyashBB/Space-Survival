namespace SpaceSurvival;

public class Loot : Sprite
{
    public Vector2 Coords { get; private set; }
    public Color Color { get; private set; }

    public Loot(Texture2D tex, Vector2 pos, Color color, float scale) : base(tex, pos, scale)
    {
        Color = color;
        Coords = pos;
        Position = new Vector2(Coords.X * tex.Width * scale, Coords.Y * tex.Height * scale);
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1f);
    }
}