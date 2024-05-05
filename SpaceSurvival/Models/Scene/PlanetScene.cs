using System;
using System.Linq;

namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private readonly MapGenerate _map;
    private readonly HeroForMapGenerator _player;
    private const int Scale = 5;

    private readonly PlanetEnemyManager _planetEnemyManager;
    private readonly LootManager _lootManager;

    private Matrix _translation;

    public PlanetScene(TypePlanet typePlanet)
    {
        _map = new MapGenerate(Scale, typePlanet);
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"),
            _map.GetRandomEmptyCell(), Scale, _map.MapCells);
        UpdatePlayerFieldOfView();

        _lootManager = new LootManager(_map, Scale);
        _lootManager.AddLoot(LootType.Type1, 2);
        _lootManager.AddLoot(LootType.Type2, 4);
        _lootManager.AddLoot(LootType.Type3, 7);

        _planetEnemyManager = new PlanetEnemyManager(_player, _map, Scale);
        _planetEnemyManager.CreateEnemies(10);
    }


    protected override void Load()
    {
    }

    public override void Activate()
    {
    }

    private void UpdatePlayerFieldOfView()
    {
        _map.Map.ComputeFov((int)_player.Coords.X, (int)_player.Coords.Y, 10, true);
    }

    private void CalculateTranslation()
    {
        var dx = _player.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, _map.MapSize.X - Globals.WindowSize.X);
        var dy = _player.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, _map.MapSize.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0f);
    }

    public override void Update()
    {
        UpdatePlayerFieldOfView();
        _player.Update(_planetEnemyManager.Enemies);
        _planetEnemyManager.Update();
        CalculateTranslation();


        var loot = _lootManager.Loots.FirstOrDefault(loot => _player.Coords == loot.Coords);
        if (loot != null)
            _lootManager.Loots.Remove(loot);
    }

    protected override void Draw()
    {
        _map.Draw();
        _lootManager.Draw();
        _planetEnemyManager.Draw();
        _player.Draw();
    }

    public override RenderTarget2D GetFrame()
    {
        Globals.GraphicsDevice.SetRenderTarget(Target);
        Globals.GraphicsDevice.Clear(Color.Black);

        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _translation);
        Draw();
        Globals.SpriteBatch.End();

        Globals.GraphicsDevice.SetRenderTarget(null);
        return Target;
    }
}