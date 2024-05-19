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
    
    private SmallBluePanel _smallBluePanel;

    private Matrix _translation;

    public PlanetScene(TypePlanet typePlanet)
    {
        _map = new MapGenerate(Scale, typePlanet);
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"),
            _map.GetRandomEmptyCell(), Scale, _map);

        _lootManager = new LootManager(_map, Scale);
        _lootManager.AddLoot(LootType.Type1, 2);
        _lootManager.AddLoot(LootType.Type2, 4);
        _lootManager.AddLoot(LootType.Type3, 7);

        _planetEnemyManager = new PlanetEnemyManager(_player, _map, Scale);
        _planetEnemyManager.CreateEnemies(10);

        _smallBluePanel =
            new SmallBluePanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), _player.Position, 0.4f);
    }


    protected override void Load()
    {
    }

    public override void Activate()
    {
        _smallBluePanel =
            new SmallBluePanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), _player.Position, 0.4f);
    }


    private void CalculateTranslation()
    {
        var dx = _player.Position.X - Globals.WindowSize.X / 2f + _player.Size.X / 2f;
        dx = MathHelper.Clamp(dx, 0, _map.MapSize.X - Globals.WindowSize.X);
        var dy = _player.Position.Y - Globals.WindowSize.Y / 2f + _player.Size.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, _map.MapSize.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0f);
    }

    private void CheckCollisionWithLoot()
    {
        var loot = _lootManager.Loots.FirstOrDefault(loot => _player.Coords == loot.Coords);
        if (loot != null && InventoryManager.CapacityPlayerInventory > 0)
        {
            InventoryManager.AddToPlayerInventory(loot);
            _lootManager.Loots.Remove(loot);
            _smallBluePanel.FillCellWithLoot();
        }
    }

    public override void Update()
    {
        _player.Update(_planetEnemyManager.Enemies);
        _planetEnemyManager.Update();
        _smallBluePanel.Update(_player.Position, _player.Size, _map.MapSize);
        CalculateTranslation();
        CheckCollisionWithLoot();
    }

    protected override void Draw()
    {
        _map.Draw();
        _lootManager.Draw();
        _planetEnemyManager.Draw();
        _player.Draw();
        _smallBluePanel.Draw();
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