using System;

namespace SpaceSurvival;

public class GameManager
{
    private readonly SceneManager _sceneManager;
    public Ship Ship { get; }

    public GameManager()
    {
        Ship = new Ship(Globals.Content.Load<Texture2D>("tiny_ship8"), Vector2.Zero, 1f);
        _sceneManager = new SceneManager(this);
    }


    public void Update()
    {
        if (InputManager.KeyPressed(Keys.Z))
            _sceneManager.SwitchScene(Scenes.SpaceScene);
        if (InputManager.KeyPressed(Keys.X))
            _sceneManager.SwitchScene(Scenes.PlanetScene);
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