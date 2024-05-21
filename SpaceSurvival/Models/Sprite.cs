namespace SpaceSurvival;

public class Sprite
{
    public Point Size { get; set; }
    public Vector2 Position;
    protected float Scale;
    protected readonly Texture2D Texture;
    public Vector2 Origin;
    protected Color Color;
    public int Speed;
    public float Rotation = 0;

    public Sprite(Texture2D tex, Vector2 pos, float scale)
    {
        Texture = tex;
        Position = pos;
        Scale = scale;
        Color = Color.White;
        Origin = new Vector2(Texture.Width * Scale / 2f, Texture.Height * Scale / 2f);
        Size = new Point((int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color, Rotation, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}