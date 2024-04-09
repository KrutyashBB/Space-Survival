namespace SpaceSurvival;

public class SpaceBackground : Sprite
{
    public SpaceBackground(Texture2D tex, Vector2 pos, float scale = 1f) : base(tex, pos, scale)
    {
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}