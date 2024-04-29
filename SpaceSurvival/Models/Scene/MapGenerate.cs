using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class MapGenerate
{
    public static Map Map;
    public Point MapSize;
    private const int MapWidth = 70;
    private const int MapHeight = 35;

    private readonly bool[,] _mapCells = new bool[MapHeight, MapWidth];
    
    private readonly int _tileWidth;
    private readonly int _tileHeight;
    private readonly int _scale;

    private readonly Texture2D _tileSet;
    private readonly Rectangle _isWalkableTex;
    private readonly Rectangle _isNotWalkableTex;

    public MapGenerate(int scale, Texture2D tileSet, Rectangle isWalkableTex, Rectangle isNotWalkableTex)
    {
        Map = Map.Create(new CaveMapCreationStrategy<Map>(MapWidth, MapHeight, 45, 2, 20));
        _scale = scale;
        _tileWidth = isWalkableTex.Width * _scale;
        _tileHeight = isWalkableTex.Height * _scale;

        MapSize = new Point(MapWidth * _tileWidth, MapHeight * _tileHeight);

        _tileSet = tileSet;
        _isWalkableTex = isWalkableTex;
        _isNotWalkableTex = isNotWalkableTex;

        foreach (var cell in Map.GetAllCells())
        {
            if (cell.IsWalkable)
                _mapCells[cell.Y, cell.X] = true;
            else
                _mapCells[cell.Y, cell.X] = false;
        }
    }


    public Vector2 GetRandomEmptyCell()
    {
        var random = new Random();
        while (true)
        {
            var x = random.Next(MapWidth - 1);
            var y = random.Next(MapHeight - 1);
            // if (Map.IsWalkable(x, y))
            // {
            //     var cell = Map.GetCell(x, y);
            //     return new Vector2(cell.X, cell.Y);
            // }
            if (_mapCells[y, x])
            {
                return new Vector2(x, y);
            }
        }
    }

    public void Draw()
    {
        for (var y = 0; y < _mapCells.GetLength(0); y++)
        {
            for (var x = 0; x < _mapCells.GetLength(1); x++)
            {
                var tileX = x * _tileWidth;
                var tileY = y * _tileHeight;
                
                Globals.SpriteBatch.Draw(_tileSet, new Vector2(tileX, tileY),
                    _mapCells[y, x] ? _isWalkableTex : _isNotWalkableTex, Color.White, 0f,
                    Vector2.Zero, _scale, SpriteEffects.None, 1f);
            }
        }
    }
}