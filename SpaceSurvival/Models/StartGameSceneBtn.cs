using System;

namespace SpaceSurvival;

public class StartGameSceneBtn : Sprite
{
    private readonly SpriteFont _font;
    private readonly string _textBtn;
    private Rectangle Rect { get; set; }

    public StartGameSceneBtn(string textBtn, Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _font = Globals.Content.Load<SpriteFont>("font");
        _textBtn = textBtn;
    }

    public void Update(Vector2 pos)
    {
        Position = pos;
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);

        if (Rect.Contains(InputManager.MousePosition))
        {
            if (InputManager.ClickedMouseLeftButton())
                Click();

            Color = Color.Gray;
        }
        else
            Color = Color.White;
    }

    public event EventHandler OnClick;

    private void Click()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    public override void Draw()
    {
        base.Draw();
        Globals.SpriteBatch.DrawString(_font, _textBtn,
            new Vector2(Position.X + 52, Position.Y + 28),
            Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
    }
}