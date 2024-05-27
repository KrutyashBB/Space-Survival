using System;

namespace SpaceSurvival;

public class AboutScene : Scene
{
    private StartGameSceneBtn _returnBtn;

    private SoundEffectInstance _history;
    private SoundEffectInstance _clickBtnSound;


    protected override void Load()
    {
        _history = Globals.Content.Load<SoundEffect>("Audio/history").CreateInstance();

        _returnBtn = new StartGameSceneBtn("    Return",
            Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            new Vector2(Globals.WindowSize.X / 2f - 180, Globals.WindowSize.Y / 2f + 200), 0.3f);
        _returnBtn.OnClick += Return;

        _clickBtnSound = Globals.Content.Load<SoundEffect>("Audio/clickBtnSound").CreateInstance();
    }

    public override void Activate()
    {
        _history.Play();
    }

    private void Return(object sender, EventArgs e)
    {
        _history.Stop();
        _clickBtnSound.Play();
        SceneManager.SwitchScene((int)TypeScene.StartGameScene);
    }

    public override void Update()
    {
        _returnBtn.Update(new Vector2(Globals.WindowSize.X / 2f - 180, Globals.WindowSize.Y / 2f));
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("bg-space"), Vector2.Zero, null, Color.White, 0f,
            Vector2.Zero, 5f, SpriteEffects.None, 0f);
        _returnBtn.Draw();
    }
}