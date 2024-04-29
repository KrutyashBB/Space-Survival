using System;
using System.Linq;

namespace SpaceSurvival;

public class GreenPlanet : Scene
{
    private MapGenerate _map;
    private HeroForMapGenerator _player;
    private int Scale = 3;

    private EnemyManager _enemyManager;
    private LootManager _lootManager;

    private Matrix _translation;
    private readonly Random _rand = new();

    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("BasicGreen");

    private readonly Rectangle[] _isWalkableTexturesSource =
    {
        new(16, 32, 16, 16),
        new(32, 32, 16, 16),
        new(16, 48, 16, 16),
    };

    private readonly Rectangle[] _isNotWalkableTexturesSource =
    {
        new(32, 0, 16, 16),
        new(112, 32, 16, 16),
    };

    public GreenPlanet(GameManager gameManager, int scale) : base(gameManager)
    {
        Scale = scale;
    }

    protected override void Load()
    {
    }


    public override void Activate()
    {
        var randWalkableTex = _isWalkableTexturesSource[_rand.Next(_isWalkableTexturesSource.Length)];
        var randNotWalkableTex = _isNotWalkableTexturesSource[_rand.Next(_isNotWalkableTexturesSource.Length)];

        _map = new MapGenerate(Scale, _tileSetForMap, randWalkableTex, randNotWalkableTex);
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"), _map.GetRandomEmptyCell(), Scale);
        UpdatePlayerFieldOfView();

        _lootManager = new LootManager(_map, Scale);
        _lootManager.AddLoot(LootType.Type1, 4);
        _lootManager.AddLoot(LootType.Type2, 8);
        _lootManager.AddLoot(LootType.Type3, 2);

        _enemyManager = new EnemyManager(_player, _map, Scale);
        _enemyManager.CreateEnemies(6);

        CombatManager.Init(_player, _enemyManager.GetEnemies());
    }

    private void UpdatePlayerFieldOfView()
    {
        MapGenerate.Map.ComputeFov((int)_player.Coords.X, (int)_player.Coords.Y, 10, true);
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
        _player.Update();
        _enemyManager.Update();
        CalculateTranslation();


        var loot = _lootManager.Loots.FirstOrDefault(loot => _player.Coords == loot.Coords);
        if (loot != null)
            _lootManager.Loots.Remove(loot);
    }

    protected override void Draw()
    {
        _map.Draw();
        _lootManager.Draw();
        _enemyManager.Draw();
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