using System;
using System.Linq;

namespace SpaceSurvival;

public class PlanetScene : Scene
{
    protected MapGenerate Map;
    protected HeroForMapGenerator Player;
    protected int Scale = 5;

    protected EnemyManager EnemyManager;
    protected LootManager LootManager;

    private Matrix _translation;
    private readonly Random _rand = new();
    
    protected Texture2D TileSetForMap;
    protected Rectangle[] WalkableTexturesSource;
    protected Rectangle[] NotWalkableTexturesSource;

    public PlanetScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
    }


    public override void Activate()
    {
        var randWalkableTex = WalkableTexturesSource[_rand.Next(WalkableTexturesSource.Length)];
        var randNotWalkableTex = NotWalkableTexturesSource[_rand.Next(NotWalkableTexturesSource.Length)];

        Map = new MapGenerate(Scale, TileSetForMap, randWalkableTex, randNotWalkableTex);
        Player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"), Map.GetRandomEmptyCell(), Scale);
        UpdatePlayerFieldOfView();
    }

    private void UpdatePlayerFieldOfView()
    {
        MapGenerate.Map.ComputeFov((int)Player.Coords.X, (int)Player.Coords.Y, 10, true);
    }

    private void CalculateTranslation()
    {
        var dx = Player.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, Map.MapSize.X - Globals.WindowSize.X);
        var dy = Player.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, Map.MapSize.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0f);
    }

    public override void Update()
    {
        UpdatePlayerFieldOfView();
        Player.Update();
        EnemyManager.Update();
        CalculateTranslation();


        var loot = LootManager.Loots.FirstOrDefault(loot => Player.Coords == loot.Coords);
        if (loot != null)
            LootManager.Loots.Remove(loot);
    }

    protected override void Draw()
    {
        Map.Draw();
        LootManager.Draw();
        EnemyManager.Draw();
        Player.Draw();
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