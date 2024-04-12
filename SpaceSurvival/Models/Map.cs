using System;

namespace SpaceSurvival;

public class Map
{
    private readonly Point _mapSize = new(6, 4);
    private readonly Point _tileSize;
    private readonly Vector2 _mapOffset = new(1.5f, 0.5f);
    private readonly Sprite[,] _tiles;

    public Map()
    {
        _tiles = new Sprite[_mapSize.X, _mapSize.Y];

        var textures = new[]
        {
            Globals.Content.Load<Texture2D>("Tile1"),
            Globals.Content.Load<Texture2D>("Tile2"),
        };

        _tileSize.X = textures[0].Width;
        _tileSize.Y = textures[0].Height / 2;

        var random = new Random();

        for (var y = 0; y < _mapSize.Y; y++)
        {
            for (var x = 0; x < _mapSize.X; x++)
            {
                var r = random.Next(0, textures.Length);
                _tiles[x, y] = new Sprite(textures[r], MapToScreen(x, y), 1f);
            }
        }
    }

    private Vector2 MapToScreen(int mapX, int mapY)
    {
        var screenX = ((mapX - mapY) * _tileSize.X / 2f) + (_mapOffset.X * _tileSize.X);
        var screenY = ((mapY + mapX) * _tileSize.Y / 2f) + (_mapOffset.Y * _tileSize.Y);

        return new Vector2(screenX, screenY);
    }

    public void Draw()
    {
        for (var y = 0; y < _mapSize.Y; y++)
        for (var x = 0; x < _mapSize.X; x++)
            _tiles[x, y].Draw();
    }
}