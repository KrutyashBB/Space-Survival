namespace SpaceSurvival;

public class Loot : Sprite
{
    private readonly LootType _type;
    private readonly int _value;
    private const int MapCellTexSize = 16;
    public Vector2 Coords { get; private set; }
    

    public Loot(LootType type, int value, Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _type = type;
        _value = value;
        Coords = pos;
        Origin = new Vector2(MapCellTexSize * Scale / 2f, MapCellTexSize * Scale / 2f);
        Size = new Point((int)(MapCellTexSize * Scale), (int)(MapCellTexSize * Scale));
        Position = new Vector2(Coords.X * MapCellTexSize * scale, Coords.Y * MapCellTexSize * scale);
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y),
            null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
    }
}