namespace SpaceSurvival;

public class GameManager
{
    public GameManager()
    {
        SceneManager.Init();
    }

    public void Update()
    {
        if (InputManager.KeyboardKeyPressed(Keys.Z))
            SceneManager.SwitchScene((int)TypeScene.SpaceScene);
        else if (InputManager.KeyboardKeyPressed(Keys.X))
            SceneManager.SwitchScene((int)TypeScene.PlayerDeathScene);


        InputManager.Update();
        SceneManager.Update();
    }

    public void Draw()
    {
        var frame = SceneManager.GetFrame();
        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Globals.SpriteBatch.Draw(frame, Vector2.Zero, Color.White);
        Globals.SpriteBatch.End();
    }
}