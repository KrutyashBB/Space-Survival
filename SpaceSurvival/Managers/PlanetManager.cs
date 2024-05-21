using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceSurvival
{
    public static class PlanetManager
    {
        public static List<PlanetSprite> Planets { get; private set; }
        private static List<(TypePlanet, Texture2D)> _textures;
        private static int _columns;
        private static int _rows;
        private static int _padding;
        private static int _maxOffset;
        private static Random _random;

        private static int _mapWidth;
        private static int _mapHeight;

        private const int PlanetScale = 1;

        private static bool _isStorePlanetCreated;

        public static void Init(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;

            Planets = new List<PlanetSprite>();
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

            _random = new Random();
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
            for (var row = 0; row <= _rows; row++)
            {
                for (var column = 0; column <= _columns; column += _random.Next(2, 5))
                {
                    var textureIndex = _random.Next(0, _textures.Count);
                    if (_textures[textureIndex].Item1 == TypePlanet.Store && _isStorePlanetCreated)
                        textureIndex = _random.Next(0, _textures.Count - 1);

                    var posX = column * (_textures[textureIndex].Item2.Width + _padding);
                    var posY = row * (_textures[textureIndex].Item2.Height + _padding);

                    posX += _random.Next(-_maxOffset, _maxOffset + 1);
                    posY += _random.Next(-_maxOffset, _maxOffset + 1);

                    posX = (int)MathHelper.Clamp(posX, _textures[textureIndex].Item2.Width * PlanetScale / 2f,
                        _mapWidth);
                    posY = (int)MathHelper.Clamp(posY, _textures[textureIndex].Item2.Height * PlanetScale / 2f + 100,
                        _mapHeight);
                    var position = new Vector2(posX, posY);

                    Planets.Add(new PlanetSprite(SceneManager.Id, _textures[textureIndex].Item1,
                        _textures[textureIndex].Item2, position, PlanetScale));

                    if (_textures[textureIndex].Item1 == TypePlanet.Store)
                    {
                        SceneManager.AddSpaceStoreScene();
                        _isStorePlanetCreated = true;
                    }
                    else
                        SceneManager.AddPlanetScene(_textures[textureIndex].Item1);
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