using System;

namespace SpaceSurvival;

public class SpacewalkBtn : Sprite
{
    private readonly Texture2D _spaceTexture;
    private Rectangle _rect;

    private readonly SpriteFont _font;

    public SpacewalkBtn(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _spaceTexture = Globals.Content.Load<Texture2D>("spaceIcon");
        _font = Globals.Content.Load<SpriteFont>("font");

        _rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
    }


    public void UpdatePosition(Vector2 pos) =>
        Position = pos;

    public void Update()
    {
        if (_rect.Contains(InputManager.MousePosition))
        {
            if (InputManager.ClickedMouseLeftButton())
                Click();
            Color = Color.Gray;
        }
        else
            Color = Color.White;
    }

    public event EventHandler OnClick;

    private void Click() =>
        OnClick?.Invoke(this, EventArgs.Empty);

    public override void Draw()
    {
        base.Draw();
        Globals.SpriteBatch.Draw(_spaceTexture, new Vector2(Position.X + (Size.X - _spaceTexture.Width * 0.8f) / 2f,
                Position.Y + (Size.Y - _spaceTexture.Height * 0.8f) / 2f), null, Color.White, 0f,
            Vector2.Zero, 0.8f, SpriteEffects.None, 0f);

        Globals.SpriteBatch.DrawString(_font, "Go Back", new Vector2(Position.X, Position.Y + Size.Y),
            Color.White, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);
    }
}