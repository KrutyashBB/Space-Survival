namespace SpaceSurvival;

public class UiBtn : Sprite
{
    private readonly SpriteFont _font = Globals.Content.Load<SpriteFont>("font");
    private Rectangle Rect { get; set; }

    public UiBtn(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
    }

    public override void Draw()
    {
        Position = new Vector2(Globals.WindowSize.X / 2f - 170, Globals.WindowSize.Y / 2f);
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
        if (Rect.Intersects(new Rectangle(InputManager.MouseState.Position, new Point(1, 1))))
        {
            Color = Color.Gray;
        }
        else
        {
            Color = Color.White;
        }

        base.Draw();
        Globals.SpriteBatch.DrawString(_font, "Start Over", new Vector2(Position.X + 107, Position.Y + 37),
            Color.White);
    }
}