using System.Collections.Generic;

namespace SpaceSurvival;

public class SceneManager
{
    private static Scenes ActiveScene { get; set; }
    private static readonly Dictionary<Scenes, Scene> _scenes = new();

    public SceneManager(GameManager gameManager)
    {
        _scenes.Add(Scenes.SpaceScene, new SpaceScene(gameManager));
        _scenes.Add(Scenes.GreenPlanet, new GreenPlanet(gameManager));
        _scenes.Add(Scenes.RedPlanet, new RedPlanet(gameManager));
        _scenes.Add(Scenes.IcePlanet, new IcePlanet(gameManager));
        _scenes.Add(Scenes.VioletPlanet, new VioletPlanet(gameManager));
        
        ActiveScene = Scenes.SpaceScene;
        _scenes[ActiveScene].Activate();
    }

    public static void SwitchScene(Scenes scene)
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