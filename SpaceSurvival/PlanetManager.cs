using System;
using System.Collections.Generic;

namespace SpaceSurvival
{
    public static class PlanetManager
    {
        public static List<Planet> Planets { get; } = new();
        private static Texture2D[] _textures;
        private static Random _random;
        private static int _columns;
        private static int _rows;
        private static int _padding;
        private static int _maxOffset;

        public static void Init(int mapWidth, int mapHeight)
        {
            _random = new Random();
            _textures = new[]
            {
                Globals.Content.Load<Texture2D>("Planet1"),
                Globals.Content.Load<Texture2D>("Planet2"),
                Globals.Content.Load<Texture2D>("Planet3"),
                Globals.Content.Load<Texture2D>("Planet4")
            };
            _padding = _textures[0].Width * 2;
            _maxOffset = _padding / 2;
            DetermineGridSize(mapWidth, mapHeight);
        }

        private static void DetermineGridSize(int mapWidth, int mapHeight)
        {
            var textureWidth = _textures[0].Width;
            var textureHeight = _textures[0].Height;

            _columns = mapWidth / (textureWidth + _padding);
            _rows = mapHeight / (textureHeight + _padding);
        }

        public static void CreatePlanets()
        {
            for (var row = 1; row < _rows; row++)
            {
                for (var column = 1; column < _columns; column++)
                {
                    var posX = column * (_textures[0].Width + _padding);
                    var posY = row * (_textures[0].Height + _padding);

                    posX += _random.Next(-_maxOffset, _maxOffset + 1);
                    posY += _random.Next(-_maxOffset, _maxOffset + 1);

                    var textureIndex = _random.Next(0, _textures.Length);
                    var position = new Vector2(posX, posY);

                    Planets.Add(new Planet(_textures[textureIndex], position, 1f));
                }
            }

            Console.WriteLine(Planets.Count);
        }

        public static void Update()
        {
            foreach (var planet in Planets)
            {
                planet.Update();
            }
        }

        public static void Draw()
        {
            foreach (var planet in Planets)
                planet.Draw();
        }
    }
}