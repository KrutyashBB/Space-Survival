using System.Collections.Generic;

namespace SpaceSurvival;

public class SceneManager
{
    public Scenes ActiveScene { get; private set; }
    private readonly Dictionary<Scenes, Scene> _scenes = new();

    public SceneManager(GameManager gameManager)
    {
        _scenes.Add(Scenes.SpaceScene, new SpaceScene(gameManager));
        _scenes.Add(Scenes.PlanetScene, new PlanetScene(gameManager));
        
        ActiveScene = Scenes.SpaceScene;
        _scenes[ActiveScene].Activate();
    }

    public void SwitchScene(Scenes scene)
    {
        ActiveScene = scene;
        _scenes[ActiveScene].Activate();
    }

    public void Update()
    {
        _scenes[ActiveScene].Update();
    }

    public RenderTarget2D GetFrame()
    {
        return _scenes[ActiveScene].GetFrame();
    }
}