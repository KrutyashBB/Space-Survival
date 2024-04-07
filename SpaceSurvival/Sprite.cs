namespace SpaceSurvival;

public class Sprite
{
    protected readonly Texture2D Texture;
    protected readonly Vector2 Origin;
    protected Vector2 Position;
    protected readonly int Speed = 300;

    public Sprite(Texture2D tex, Vector2 pos)
    {
        Texture = tex;
        Position = pos;
        Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 1f);
    }
}