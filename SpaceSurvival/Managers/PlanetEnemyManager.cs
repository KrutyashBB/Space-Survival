using System.Collections.Generic;

namespace SpaceSurvival;

public class PlanetEnemyManager
{
    public List<Enemy> Enemies { get; } = new();

    private readonly HeroForMapGenerator _hero;
    private readonly MapGenerate _map;

    private readonly int _scale;

    public PlanetEnemyManager(HeroForMapGenerator hero, MapGenerate map, int scale)
    {
        _map = map;
        _hero = hero;
        _scale = scale;
    }

    public void CreateEnemies(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var enemyCoords = _map.GetRandomEmptyCell();
            var pathToPlayer =
                new PathToPlayer(_hero, _map.Map, Globals.Content.Load<Texture2D>("path"), _scale);
            pathToPlayer.CreateFrom((int)enemyCoords.X, (int)enemyCoords.Y);
            var enemy = new Enemy(Globals.Content.Load<Texture2D>("enemy"), enemyCoords, _scale,
                pathToPlayer, _map.Map);
            Enemies.Add(enemy);
        }
    }


    public void Update()
    {
        foreach (var enemy in Enemies)
            enemy.Update();

        Enemies.RemoveAll(enemy => enemy.Health <= 0);
    }

    public void Draw()
    {
        foreach (var enemy in Enemies)
        {
            if (Globals.DebugFlag)
                if (!_map.Map.IsInFov((int)enemy.Coords.X, (int)enemy.Coords.Y)) continue;
            enemy.Draw();
        }
    }
}