using System;

namespace SpaceSurvival;

public class InventoryBtn : Sprite
{
    private readonly SpriteFont _font;
    private readonly Texture2D _shipTexture;

    public InventoryBtn(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _font = Globals.Content.Load<SpriteFont>("font");
        _shipTexture = Globals.Content.Load<Texture2D>("tiny_ship8");
    }

    private static Vector2 CalculatePosition(Vector2 playerPos, Point mapSize)
    {
        var dx = playerPos.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, mapSize.X - Globals.WindowSize.X);
        var dy = playerPos.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, mapSize.Y - Globals.WindowSize.Y);
        return new Vector2(dx, dy);
    }

    public void Update(Vector2 shipPos, Point mapSize)
    {
        Position = CalculatePosition(shipPos, mapSize);
    }

    public override void Draw()
    {
        base.Draw();
        Globals.SpriteBatch.Draw(_shipTexture, new Vector2(Position.X + (Size.X - _shipTexture.Width * 0.8f) / 2f,
                Position.Y + (Size.Y - _shipTexture.Height * 0.8f) / 2f), null, Color.White, 0f,
            Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
        
        Globals.SpriteBatch.DrawString(_font, "Click TAB", new Vector2(Position.X, Position.Y + Size.Y), Color.White);
    }
}