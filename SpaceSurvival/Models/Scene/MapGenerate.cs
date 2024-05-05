using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class MapGenerate
{
    public Map Map { get; private set; }
    public Point MapSize;
    private const int MapWidth = 70;
    private const int MapHeight = 35;

    public bool[,] MapCells { get; private set; } = new bool[MapHeight, MapWidth];

    private readonly int _tileWidth;
    private readonly int _tileHeight;
    private readonly int _scale;

    private Texture2D _tileSet;
    private Rectangle _isWalkableTex;
    private Rectangle _isNotWalkableTex;

    private Random _random = new();

    public MapGenerate(int scale, TypePlanet typePlanet)
    {
        SetTileSet(typePlanet);

        Map = Map.Create(new CaveMapCreationStrategy<Map>(MapWidth, MapHeight, 45, 2, 20));
        _scale = scale;
        _tileWidth = _isWalkableTex.Width * _scale;
        _tileHeight = _isWalkableTex.Height * _scale;

        MapSize = new Point(MapWidth * _tileWidth, MapHeight * _tileHeight);

        foreach (var cell in Map.GetAllCells())
        {
            if (cell.IsWalkable)
                MapCells[cell.Y, cell.X] = true;
            else
                MapCells[cell.Y, cell.X] = false;
        }
    }

    private void SetTileSet(TypePlanet typePlanet)
    {
        if (typePlanet == TypePlanet.Green)
            SetGreenTileSet();
        if (typePlanet == TypePlanet.Ice)
            SetIceTileSet();
        if (typePlanet == TypePlanet.Red)
            SetRedTileSet();
        if (typePlanet == TypePlanet.Violet)
            SetVioletTileSet();
    }

    private void SetGreenTileSet()
    {
        _tileSet = Globals.Content.Load<Texture2D>("BasicGreen");

        var walkableTexturesSource = new Rectangle[]
        {
            new(16, 32, 16, 16),
            new(32, 32, 16, 16),
            new(16, 48, 16, 16),
        };

        var notWalkableTexturesSource = new Rectangle[]
        {
            new(32, 0, 16, 16),
            new(112, 32, 16, 16),
        };

        _isWalkableTex = walkableTexturesSource[_random.Next(walkableTexturesSource.Length)];
        _isNotWalkableTex = notWalkableTexturesSource[_random.Next(notWalkableTexturesSource.Length)];
    }

    private void SetIceTileSet()
    {
        _tileSet = Globals.Content.Load<Texture2D>("IceSet");

        var walkableTexturesSource = new Rectangle[]
        {
            new(48, 64, 16, 16),
        };

        var notWalkableTexturesSource = new Rectangle[]
        {
            new(0, 0, 16, 16),
            new(16, 0, 16, 16),
            new(32, 0, 16, 16),
            new(0, 48, 16, 16),
            new(64, 32, 16, 16),
            new(16, 48, 16, 16),
        };

        _isWalkableTex = walkableTexturesSource[_random.Next(walkableTexturesSource.Length)];
        _isNotWalkableTex = notWalkableTexturesSource[_random.Next(notWalkableTexturesSource.Length)];
    }

    private void SetRedTileSet()
    {
        _tileSet = Globals.Content.Load<Texture2D>("FireSet");

        var walkableTexturesSource = new Rectangle[]
        {
            new(64, 16, 16, 16),
            new(48, 48, 16, 16),
        };

        var notWalkableTexturesSource = new Rectangle[]
        {
            new(48, 0, 16, 16),
            new(48, 16, 16, 16),
            new(48, 32, 16, 16),
            new(64, 32, 16, 16),
        };

        _isWalkableTex = walkableTexturesSource[_random.Next(walkableTexturesSource.Length)];
        _isNotWalkableTex = notWalkableTexturesSource[_random.Next(notWalkableTexturesSource.Length)];
    }

    private void SetVioletTileSet()
    {
        _tileSet = Globals.Content.Load<Texture2D>("DarkCastle");

        var walkableTexturesSource = new Rectangle[]
        {
            new(32, 64, 16, 16),
            new(48, 64, 16, 16),
            new(48, 48, 16, 16),
        };

        var notWalkableTexturesSource = new Rectangle[]
        {
            new(0, 0, 16, 16),
            new(64, 48, 16, 16),
        };

        _isWalkableTex = walkableTexturesSource[_random.Next(walkableTexturesSource.Length)];
        _isNotWalkableTex = notWalkableTexturesSource[_random.Next(notWalkableTexturesSource.Length)];
    }


    public Vector2 GetRandomEmptyCell()
    {
        var random = new Random();
        while (true)
        {
            var x = random.Next(MapWidth - 1);
            var y = random.Next(MapHeight - 1);
            if (Map.IsWalkable(x, y))
            {
                var cell = Map.GetCell(x, y);
                return new Vector2(cell.X, cell.Y);
            }
        }
    }

    private bool flag;
    public void Draw()
    {
        if (InputManager.KeyPressed(Keys.V))
            flag = !flag;
        
        for (var y = 0; y < MapCells.GetLength(0); y++)
        {
            for (var x = 0; x < MapCells.GetLength(1); x++)
            {
                var tileX = x * _tileWidth;
                var tileY = y * _tileHeight;
                

                if (flag)
                    if (!Map.IsInFov(x, y)) 
                        continue;
                

                Globals.SpriteBatch.Draw(_tileSet, new Vector2(tileX, tileY),
                    MapCells[y, x] ? _isWalkableTex : _isNotWalkableTex, Color.White, 0f,
                    Vector2.Zero, _scale, SpriteEffects.None, 1f);
            }
        }
    }
}