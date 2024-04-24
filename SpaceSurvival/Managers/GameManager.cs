namespace SpaceSurvival;

public class GameManager
{
    private readonly SceneManager _sceneManager;

    public GameManager()
    {
        _sceneManager = new SceneManager(this);
    }


    public void Update()
    {
        if (InputManager.KeyPressed(Keys.Z))
            SceneManager.SwitchScene(Scenes.SpaceScene);
        if (InputManager.KeyPressed(Keys.X))
            SceneManager.SwitchScene(Scenes.PlanetScene);
        if (InputManager.KeyPressed(Keys.C))
            SceneManager.SwitchScene(Scenes.GreenPlanet);
        InputManager.Update();
        _sceneManager.Update();
    }

    public void Draw()
    {
        var frame = _sceneManager.GetFrame();
        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Globals.SpriteBatch.Draw(frame, Vector2.Zero, Color.White);
        Globals.SpriteBatch.End();
    }
}