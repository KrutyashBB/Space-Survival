using System;

namespace SpaceSurvival;

public class IcePlanet : Scene
{
    private MapGenerate map;
    private HeroForMapGenerator _player;
    private int Scale = 4;

    private Matrix _translation;
    private readonly Random _rand = new();

    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("IceSet");

    private readonly Rectangle[] _isWalkableTexturesSource =
    {
        new(48, 64, 16, 16),
    };

    private readonly Rectangle[] _isNotWalkableTexturesSource =
    {
        new(0, 0, 16, 16),
        new(16, 0, 16, 16),
        new(32, 0, 16, 16),
        new(0, 48, 16, 16),
        new(64, 32, 16, 16),
        new(16, 48, 16, 16),
    };
    public IcePlanet(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        
    }

    public override void Activate()
    {
        var randWalkableTex = _isWalkableTexturesSource[_rand.Next(_isWalkableTexturesSource.Length)];
        var randNotWalkableTex = _isNotWalkableTexturesSource[_rand.Next(_isNotWalkableTexturesSource.Length)];

        map = new MapGenerate(Scale, _tileSetForMap, randWalkableTex, randNotWalkableTex);
        _player = new HeroForMapGenerator(Globals.Content.Load<Texture2D>("player"), map.GetRandomEmptyCell(), Scale);
    }
    private void CalculateTranslation()
    {
        var dx = _player.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, map.MapSize.X - Globals.WindowSize.X);
        var dy = _player.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, map.MapSize.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0f);
    }

    public override void Update()
    {
        _player.Update();
        CalculateTranslation();
    }

    protected override void Draw()
    {
        map.Draw();
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