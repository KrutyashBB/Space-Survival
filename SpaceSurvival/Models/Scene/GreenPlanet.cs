﻿using System;

namespace SpaceSurvival;

public class GreenPlanet : Scene
{
    private MapGenerate _map;
    private HeroForMapGenerator _player;
    private EnemyManager _enemyManager;
    private int Scale = 1;

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

    public GreenPlanet(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        var randWalkableTex = _isWalkableTexturesSource[_rand.Next(_isWalkableTexturesSource.Length)];
        var randNotWalkableTex = _isNotWalkableTexturesSource[_rand.Next(_isNotWalkableTexturesSource.Length)];

        _map = new MapGenerate(Scale, _tileSetForMap, randWalkableTex, randNotWalkableTex);
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"), _map.GetRandomEmptyCell(), Scale);

        _enemyManager = new EnemyManager(_player, _map, Scale);
        _enemyManager.AddEnemies(6);
    }


    public override void Activate()
    {
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
        _player.Update();
        _enemyManager.Update();
        CalculateTranslation();
    }

    protected override void Draw()
    {
        _map.Draw();
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