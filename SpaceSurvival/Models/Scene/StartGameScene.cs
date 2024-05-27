using System;

namespace SpaceSurvival;

public class StartGameScene : Scene
{
    private SpriteFont _font;
    private StartGameSceneBtn _startGameBtn;
    private StartGameSceneBtn _controlSectionBtn;
    private StartGameSceneBtn _historyBtn;

    private SoundEffectInstance _song;
    private SoundEffectInstance _clickBtnSound;

    protected override void Load()
    {
        _font = Globals.Content.Load<SpriteFont>("font");

        _song = Globals.Content.Load<SoundEffect>("Audio/mainSong").CreateInstance();
        _song.Volume = 0.5f;
        _clickBtnSound = Globals.Content.Load<SoundEffect>("Audio/clickBtnSound").CreateInstance();

        _startGameBtn =
            new StartGameSceneBtn("Start Game", Globals.Content.Load<Texture2D>("Small_Blue_Panel"), Vector2.Zero,
                0.3f);
        _startGameBtn.OnClick += StartGame;

        _controlSectionBtn = new StartGameSceneBtn("   Control",
            Globals.Content.Load<Texture2D>("Small_Blue_Panel"), Vector2.Zero, 0.3f);
        _controlSectionBtn.OnClick += ControlScene;

        _historyBtn = new StartGameSceneBtn("    About  ", Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            Vector2.Zero, 0.3f);
        _historyBtn.OnClick += AboutScene;
    }

    public override void Activate()
    {
        _song.IsLooped = true;
        _song.Play();
    }

    private void StartGame(object sender, EventArgs e)
    {
        _clickBtnSound.Play();
        _song.Stop();
        SceneManager.SwitchScene((int)TypeScene.SpaceScene);
    }

    private void ControlScene(object sender, EventArgs e)
    {
        _clickBtnSound.Play();
        SceneManager.SwitchScene((int)TypeScene.ControlScene);
    }

    private void AboutScene(object sender, EventArgs e)
    {
        _clickBtnSound.Play();
        SceneManager.SwitchScene((int)TypeScene.AboutScene);
    }

    public override void Update()
    {
        _startGameBtn.Update(new Vector2(Globals.WindowSize.X / 2f - 185, Globals.WindowSize.Y / 2f - 100));
        _controlSectionBtn.Update(new Vector2(Globals.WindowSize.X / 2f - 185, Globals.WindowSize.Y / 2f + 20));
        _historyBtn.Update(new Vector2(Globals.WindowSize.X / 2f - 185, Globals.WindowSize.Y / 2f + 140));
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("bg-space"), Vector2.Zero, null, Color.White, 0f,
            Vector2.Zero, 5f, SpriteEffects.None, 0f);

        Globals.SpriteBatch.DrawString(_font, "Space Survival",
            new Vector2(Globals.WindowSize.X / 2f - 220, Globals.WindowSize.Y / 2f - 200),
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

        _controlSectionBtn.Draw();
        _startGameBtn.Draw();
        _historyBtn.Draw();
    }
}