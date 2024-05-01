namespace SpaceSurvival;

public class Sprite
{
    public Point Size { get; private set; }
    public Vector2 Position;
    protected float Scale;
    protected readonly Texture2D Texture;
    public readonly Vector2 Origin;
    public int Speed;
    public float Rotation = 0;

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
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, 1f);
    }
}