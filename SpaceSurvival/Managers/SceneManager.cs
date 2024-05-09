using System.Collections.Generic;

namespace SpaceSurvival;

public static class SceneManager
{
    private static int ActiveScene { get; set; }
    private static Dictionary<int, Scene> _scenes = new();

    public static void Init()
    {
        _scenes.Add(0, new SpaceScene());

        ActiveScene = 0;
        _scenes[ActiveScene].Activate();
    }

    public static void AddSpaceStoreScene(int id)
    {
        _scenes.Add(id, new StoreScene());
    }

    public static void AddPlanetScene(int id, TypePlanet typePlanet)
    {
        _scenes.Add(id, new PlanetScene(typePlanet));
    }

    public static void SwitchScene(int id)
    {
        ActiveScene = id;
        _scenes[ActiveScene].Activate();
    }

    public static void Update()
    {
        _scenes[ActiveScene].Update();
    }

    public static RenderTarget2D GetFrame()
    {
        return _scenes[ActiveScene].GetFrame();
    }
}