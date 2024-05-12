using System.Collections.Generic;

namespace SpaceSurvival;

public static class SceneManager
{
    public static int Id { get; private set; }
    private static int ActiveScene { get; set; }
    private static Dictionary<int, Scene> _scenes;

    public static void Init()
    {
        Id = 0;
        _scenes = new Dictionary<int, Scene>();

        ActiveScene = Id;
        _scenes.Add(Id++, new SpaceScene());
        _scenes[ActiveScene].Activate();
    }

    public static void AddSpaceStoreScene()
    {
        _scenes.Add(Id++, new StoreScene());
    }

    public static void AddPlanetScene(TypePlanet typePlanet)
    {
        _scenes.Add(Id++, new PlanetScene(typePlanet));
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