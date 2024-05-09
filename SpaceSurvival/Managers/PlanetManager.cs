using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceSurvival
{
    public static class PlanetManager
    {
        public static List<PlanetSprite> Planets { get; } = new();

        private static List<(TypePlanet, Texture2D)> _textures;
        private static Random _random;
        private static int _columns;
        private static int _rows;
        private static int _padding;
        private static int _maxOffset;

        private static int planetScale = 1;

        private static bool IsStorePlanetCreated = false;

        public static void Init(int mapWidth, int mapHeight)
        {
            _random = new Random();
            _textures = new List<(TypePlanet, Texture2D)>
            {
                (TypePlanet.Green, Globals.Content.Load<Texture2D>("greenPlanet")),
                (TypePlanet.Ice, Globals.Content.Load<Texture2D>("icePlanet")),
                (TypePlanet.Violet, Globals.Content.Load<Texture2D>("violetPlanet")),
                (TypePlanet.Red, Globals.Content.Load<Texture2D>("redPlanet")),
                (TypePlanet.Store, Globals.Content.Load<Texture2D>("SpaceStore"))
            };
            _padding = _textures[0].Item2.Width * 2;
            _maxOffset = _padding / 2;
            DetermineGridSize(mapWidth, mapHeight);
        }

        private static void DetermineGridSize(int mapWidth, int mapHeight)
        {
            var textureWidth = _textures[0].Item2.Width;
            var textureHeight = _textures[0].Item2.Height;

            _columns = mapWidth / (textureWidth + _padding);
            _rows = mapHeight / (textureHeight + _padding);
        }

        public static void CreatePlanets()
        {
            var planetId = 1;
            for (var row = 1; row < _rows; row++)
            {
                for (var column = 1; column < _columns; column++)
                {
                    var posX = column * (_textures[0].Item2.Width + _padding);
                    var posY = row * (_textures[0].Item2.Height + _padding);

                    posX += _random.Next(-_maxOffset, _maxOffset + 1);
                    posY += _random.Next(-_maxOffset, _maxOffset + 1);

                    var textureIndex = _random.Next(0, _textures.Count);
                    var position = new Vector2(posX, posY);

                    if (textureIndex == 4 && IsStorePlanetCreated)
                        textureIndex = _random.Next(0, _textures.Count - 1);

                    Planets.Add(new PlanetSprite(planetId, _textures[textureIndex].Item1, _textures[textureIndex].Item2,
                        position, planetScale));

                    if (_textures[textureIndex].Item1 == TypePlanet.Store)
                    {
                        SceneManager.AddSpaceStoreScene(planetId);
                        IsStorePlanetCreated = true;
                    }
                    else
                        SceneManager.AddPlanetScene(planetId, _textures[textureIndex].Item1);

                    planetId++;
                }
            }

            Console.WriteLine(Planets.Count);
        }

        public static void Update()
        {
            foreach (var planet in Planets)
                planet.Update();
        }

        public static void Draw()
        {
            foreach (var planet in Planets)
                planet.Draw();
        }
    }
}