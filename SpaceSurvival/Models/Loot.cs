namespace SpaceSurvival;

public class Loot : Sprite
{
    private readonly LootType _type;
    private readonly Color _color;
    private readonly int _value;
    public Vector2 Coords { get; private set; }

    public Loot(LootType type, int value, Texture2D tex, Vector2 pos, Color color, float scale) : base(tex, pos, scale)
    {
        _type = type;
        _value = value;
        _color = color;
        Coords = pos;
        Position = new Vector2(Coords.X * tex.Width * scale, Coords.Y * tex.Height * scale);
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, _color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}