using System.Collections.Generic;

namespace SpaceSurvival;

public class EnemyManager
{
    private readonly List<Enemy> _enemies = new();

    private readonly HeroForMapGenerator _hero;
    private readonly MapGenerate _map;

    private readonly int _scale;

    public EnemyManager(HeroForMapGenerator hero, MapGenerate map, int scale)
    {
        _map = map;
        _hero = hero;
        _scale = scale;
    }

    public void AddEnemies(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var enemyCoords = _map.GetRandomEmptyCell();
            var pathToPlayer = new PathToPlayer(_hero, MapGenerate.Map, Globals.Content.Load<Texture2D>("path"), _scale);
            pathToPlayer.CreateFrom((int)enemyCoords.X, (int)enemyCoords.Y);
            var enemy = new Enemy(Globals.Content.Load<Texture2D>("enemy"), enemyCoords, _scale, pathToPlayer);
            _enemies.Add(enemy);
        }
    }

    public void Update()
    {
        foreach (var enemy in _enemies)
            enemy.Update();
    }

    public void Draw()
    {
        foreach (var enemy in _enemies)
            enemy.Draw();
    }
}