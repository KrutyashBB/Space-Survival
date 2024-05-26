namespace SpaceSurvival;

public class StartOverBtn : Sprite
{
    private Rectangle Rect { get; set; }

    private readonly SpriteFont _font = Globals.Content.Load<SpriteFont>("font");
    private readonly SoundEffectInstance _clickBtnSound;

    public StartOverBtn(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        _clickBtnSound = Globals.Content.Load<SoundEffect>("Audio/clickBtnSound").CreateInstance();
    }

    public void Update()
    {
        Position = new Vector2(Globals.WindowSize.X / 2f - 170, Globals.WindowSize.Y / 2f);
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
        if (Rect.Contains(InputManager.MousePosition))
        {
            if (InputManager.ClickedMouseLeftButton())
            {
                _clickBtnSound.Play();
                SceneManager.Reset();
            }

            Color = Color.Gray;
        }
        else
            Color = Color.White;
    }

    public override void Draw()
    {
        base.Draw();
        Globals.SpriteBatch.DrawString(_font, "Start Over", new Vector2(Position.X + 107, Position.Y + 37),
            Color.White);
    }
}