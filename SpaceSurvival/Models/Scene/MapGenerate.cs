using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class MapGenerate
{
    public static Map Map;
    public Point MapSize;
    private const int MapWidth = 60;
    private const int MapHeight = 30;

    private readonly Texture2D _floor;
    private readonly Texture2D _wall;
    private static int _tileWidth;
    private static int _tileHeight;
    private readonly int _scale;

    private Texture2D _tileSet;
    private Rectangle _isWalkableTex;
    private Rectangle _isNotWalkableTex;

    public MapGenerate(int scale, Texture2D tileSet, Rectangle isWalkableTex, Rectangle isNotWalkableTex)
    {
        Map = Map.Create(new CaveMapCreationStrategy<Map>(MapWidth, MapHeight, 45, 2, 20));
        _floor = Globals.Content.Load<Texture2D>("floor");
        _wall = Globals.Content.Load<Texture2D>("wall");

        _scale = scale;
        _tileWidth = _floor.Width * _scale;
        _tileHeight = _floor.Height * _scale;

        MapSize = new Point(MapWidth * _tileWidth, MapHeight * _tileHeight);

        _tileSet = tileSet;
        _isWalkableTex = isWalkableTex;
        _isNotWalkableTex = isNotWalkableTex;
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

    public void Draw()
    {
        foreach (var cell in Map.GetAllCells())
        {
            var tileX = cell.X * _tileWidth;
            var tileY = cell.Y * _tileHeight;
            var tex = cell.IsWalkable ? _floor : _wall;
            
            Globals.SpriteBatch.Draw(_tileSet, new Vector2(tileX, tileY),
                cell.IsWalkable ? _isWalkableTex : _isNotWalkableTex, Color.White, 0f,
                Vector2.Zero, _scale, SpriteEffects.None, 1f);
        }
    }
}