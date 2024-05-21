namespace SpaceSurvival;

public class PlayerDeathScene : Scene
{
    private readonly SpriteFont _font = Globals.Content.Load<SpriteFont>("font");
    private StartOverBtn _returnToGameBtn;

    protected override void Load()
    {
        _returnToGameBtn = new StartOverBtn(Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            new Vector2(Globals.WindowSize.X / 2f - 170, Globals.WindowSize.Y / 2f), 0.3f);
    }

    public override void Activate()
    {
    }

    public override void Update()
    {
        _returnToGameBtn.Update();
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.DrawString(_font, "Game Over",
            new Vector2(Globals.WindowSize.X / 2f - 80, Globals.WindowSize.Y / 2f - 100), Color.White);

        _returnToGameBtn.Draw();
    }
}