using System;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace SpaceSurvival
{
    [Flags]
    internal enum Trans
    {
        None = 0,
        FlipH = 1 << 0,
        FlipV = 1 << 1,
        FlipD = 1 << 2,

        Rotate90 = FlipD | FlipH,
        Rotate180 = FlipH | FlipV,
        Rotate270 = FlipV | FlipD,
        Rotate90AndFlipH = FlipH | FlipV | FlipD,
    }


    public class MapRenderer
    {
        private const string MapFilePath = @"..\..\..\Content\newMap.tmx";
        private const string ContentDirectory = @"..\..\..\Content\";

        private readonly Dictionary<(TiledMapTileset, TiledTileset, int), Rectangle> _sourceRectCache = new();
        private static TiledMap _map;
        private readonly Dictionary<int, TiledTileset> _tileSets;
        private readonly Texture2D _tileSetTexture;
        private static int _tileWidth;
        private static int _tileHeight;
        public const float Scale = 4.5f;

        public static TiledLayer collisionLayer;


        public MapRenderer()
        {
            _map = new TiledMap(MapFilePath);
            _tileSets = _map.GetTiledTilesets(ContentDirectory);
            _tileSetTexture = Globals.Content.Load<Texture2D>("MasterSimple");

            _tileWidth = (int)(_map.TileWidth * Scale);
            _tileHeight = (int)(_map.TileHeight * Scale);

            collisionLayer = _map.Layers.First(l => l.name == "rocksObject");
        }

        public static List<Rectangle> GetCollisionRectangles()
        {
            return collisionLayer.objects.Select(obj => new Rectangle((int)(obj.x * Scale), (int)(obj.y * Scale),
                (int)(obj.width * Scale), (int)(obj.height * Scale))).ToList();
        }


        public void Draw()
        {
            var tileLayers = _map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
                for (var y = 0; y < layer.height; y++)
                for (var x = 0; x < layer.width; x++)
                    DrawTile(layer, x, y);
        }

        private void DrawTile(TiledLayer layer, int x, int y)
        {
            var index = y * layer.width + x;
            var gid = layer.data[index];

            var tileX = (int)(x * _map.TileWidth * Scale);
            var tileY = (int)(y * _map.TileHeight * Scale);


            if (gid == 0)
                return;

            var mapTileSet = _map.GetTiledMapTileset(gid);
            var tileSet = _tileSets[mapTileSet.firstgid];

            if (!_sourceRectCache.ContainsKey((mapTileSet, tileSet, gid)))
            {
                var rect = _map.GetSourceRect(mapTileSet, tileSet, gid);
                _sourceRectCache[(mapTileSet, tileSet, gid)] = new Rectangle(rect.x, rect.y, rect.width, rect.height);
            }

            var source = _sourceRectCache[(mapTileSet, tileSet, gid)];

            var destination = new Rectangle(tileX, tileY, _tileWidth, _tileHeight);

            var tileTrans = GetTileTransformation(layer, x, y);
            var (effects, rotation) = GetSpriteEffectsAndRotation(tileTrans, ref destination);

            Globals.SpriteBatch.Draw(_tileSetTexture, destination, source, Color.White, (float)rotation, Vector2.Zero,
                effects, 0);
        }

        private Trans GetTileTransformation(TiledLayer layer, int x, int y)
        {
            var tileTrans = Trans.None;
            if (_map.IsTileFlippedHorizontal(layer, x, y)) tileTrans |= Trans.FlipH;
            if (_map.IsTileFlippedVertical(layer, x, y)) tileTrans |= Trans.FlipV;
            if (_map.IsTileFlippedDiagonal(layer, x, y)) tileTrans |= Trans.FlipD;
            return tileTrans;
        }

        private (SpriteEffects, double) GetSpriteEffectsAndRotation(Trans tileTrans, ref Rectangle destination)
        {
            var effects = SpriteEffects.None;
            double rotation = 0f;
            switch (tileTrans)
            {
                case Trans.FlipH:
                    effects = SpriteEffects.FlipHorizontally;
                    break;
                case Trans.FlipV:
                    effects = SpriteEffects.FlipVertically;
                    break;
                case Trans.Rotate90:
                    rotation = Math.PI * .5f;
                    destination.X += (int)(_map.TileWidth * Scale);
                    break;
                case Trans.Rotate180:
                    rotation = Math.PI;
                    destination.X += (int)(_map.TileWidth * Scale);
                    destination.Y += (int)(_map.TileHeight * Scale);
                    break;
                case Trans.Rotate270:
                    rotation = Math.PI * 3 / 2;
                    destination.Y += (int)(_map.TileHeight * Scale);
                    break;
                case Trans.Rotate90AndFlipH:
                    effects = SpriteEffects.FlipHorizontally;
                    rotation = Math.PI * .5f;
                    destination.X += (int)(_map.TileWidth * Scale);
                    break;
            }

            return (effects, rotation);
        }
    }
}