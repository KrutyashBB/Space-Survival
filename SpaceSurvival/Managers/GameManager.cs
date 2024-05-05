namespace SpaceSurvival;

public class GameManager
{
    public GameManager()
    {
        SceneManager.Init();
    }


    public void Update()
    {
        if (InputManager.KeyPressed(Keys.Z))
            SceneManager.SwitchScene(0);
        
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