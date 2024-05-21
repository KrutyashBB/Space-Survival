using System;
using System.Linq;

namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private readonly MapGenerate _map;
    private static HeroForMapGenerator _player;
    private Vector2 _playerCoords;
    private Vector2 _playerPosition;
    private const int Scale = 5;

    private PlanetEnemyManager _planetEnemyManager;
    private readonly LootManager _lootManager;

    private SmallBluePanel _smallBluePanel;

    private const float Duration = 1f;
    private float _durationLeft;

    private Matrix _translation;

    public PlanetScene(TypePlanet typePlanet)
    {
        _map = new MapGenerate(Scale, typePlanet);
        _playerCoords = _map.GetRandomEmptyCell();
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"),
            _playerCoords, Scale, _map);
        _playerPosition = _player.Position;

        _lootManager = new LootManager(_map, Scale);
        _lootManager.AddLoot(LootType.Gold, 2);
        _lootManager.AddLoot(LootType.Ruby, 4);
        _lootManager.AddLoot(LootType.Bronze, 7);

        _planetEnemyManager = new PlanetEnemyManager(_player.Coords, _map, Scale);
        _planetEnemyManager.CreateEnemies(10);

        _smallBluePanel =
            new SmallBluePanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), _player.Position, 0.4f);
    }


    protected override void Load()
    {
    }

    public override void Activate()
    {
        _durationLeft = 0;
        _player.Coords = _playerCoords;
        _player.Position = _playerPosition;
        _player.Map = _map;

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
        _durationLeft += Globals.TotalSeconds;
        if (_durationLeft > Duration)
        {
            _planetEnemyManager.Update(_player.Coords);
            _smallBluePanel.Update(_player.Position, _player.Size, _map.MapSize);
            CheckCollisionWithLoot();
        }

        _player.Update(_planetEnemyManager.Enemies);
        _playerCoords = _player.Coords;
        CalculateTranslation();
    }

    protected override void Draw()
    {
        if (_durationLeft < Duration)
        {
            Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("landingBack"),
                new Rectangle((int)-_translation.Translation.X, (int)-_translation.Translation.Y, Globals.WindowSize.X,
                    Globals.WindowSize.Y), Color.White);
        }
        else
        {
            _map.Draw();
            _lootManager.Draw();
            _planetEnemyManager.Draw();
            _player.Draw();
            _smallBluePanel.Draw();
        }
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