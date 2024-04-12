namespace SpaceSurvival;

public class Sprite
{
    public Point Size { get; private set; }
    protected float Scale;
    protected readonly Texture2D Texture;
    protected readonly Vector2 Origin;
    public Vector2 Position;
    protected readonly int Speed = 300;

    public Sprite(Texture2D tex, Vector2 pos, float scale)
    {
        Texture = tex;
        Position = pos;
        Scale = scale;
        Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        Size = new Point((int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1f);
    }
}