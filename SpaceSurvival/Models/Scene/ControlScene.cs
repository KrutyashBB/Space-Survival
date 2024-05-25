using System;

namespace SpaceSurvival;

public class ControlScene : Scene
{
    private StartGameSceneBtn _returnBtn;

    private SpriteFont _font;

    protected override void Load()
    {
        _returnBtn = new StartGameSceneBtn("    Return",
            Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            new Vector2(Globals.WindowSize.X / 2f - 180, Globals.WindowSize.Y / 2f + 200), 0.3f);
        _returnBtn.OnClick += Return;

        _font = Globals.Content.Load<SpriteFont>("font");
    }

    public override void Activate()
    {
    }

    private void Return(object sender, EventArgs e)
    {
        SceneManager.SwitchScene((int)TypeScene.StartGameScene);
    }

    public override void Update()
    {
        _returnBtn.Update(new Vector2(Globals.WindowSize.X / 2f - 180, Globals.WindowSize.Y / 2f + 200));
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("bg-space"), Vector2.Zero, null, Color.White, 0f,
            Vector2.Zero, 5f, SpriteEffects.None, 0f);
        Globals.SpriteBatch.DrawString(_font, "W/A/S/D to move  ",
            new Vector2(Globals.WindowSize.X / 2f - 125, Globals.WindowSize.Y / 2f - 100), Color.White);
        Globals.SpriteBatch.DrawString(_font, "  Space to shot  ",
            new Vector2(Globals.WindowSize.X / 2f - 125, Globals.WindowSize.Y / 2f), Color.White);
        Globals.SpriteBatch.DrawString(_font, "Shift to speed up",
            new Vector2(Globals.WindowSize.X / 2f - 125, Globals.WindowSize.Y / 2f + 100), Color.White);
        _returnBtn.Draw();
    }
}