using System.Collections.Generic;

namespace SpaceSurvival;

public enum TypeScene
{
    PlayerDeathScene,
    PlayerShipScene,
    SpaceScene
}

public static class SceneManager
{
    public static int Id { get; private set; }
    private static int ActiveScene { get; set; }
    private static Dictionary<int, Scene> _scenes;

    public static void Init()
    {
        _scenes = new Dictionary<int, Scene>
        {
            { (int)TypeScene.PlayerDeathScene, new PlayerDeathScene() },
            { (int)TypeScene.PlayerShipScene, new PlayerShipScene() }
        };
        Id = _scenes.Keys.Count;

        _scenes.Add(Id++, new SpaceScene());
        ActiveScene = (int)TypeScene.SpaceScene;
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