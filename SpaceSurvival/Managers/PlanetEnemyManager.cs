﻿using System.Collections.Generic;

namespace SpaceSurvival;

public class PlanetEnemyManager
{
    public List<Enemy> Enemies { get; } = new();

    private readonly Vector2 _playerCoords;
    private readonly MapGenerate _map;

    private readonly int _scale;

    public PlanetEnemyManager(Vector2 playerCoords, MapGenerate map, int scale)
    {
        _map = map;
        _playerCoords = playerCoords;
        _scale = scale;
    }

    public void CreateEnemies(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var enemyCoords = _map.GetRandomEmptyCell();
            var pathToPlayer =
                new PathToPlayer(_playerCoords, _map.Map, Globals.Content.Load<Texture2D>("path"), _scale);
            pathToPlayer.CreateFromTO((int)enemyCoords.X, (int)enemyCoords.Y, (int)_playerCoords.X,
                (int)_playerCoords.Y);
            var enemy = new Enemy(Globals.Content.Load<Texture2D>("enemy"), enemyCoords, _scale,
                pathToPlayer, _map.Map);
            Enemies.Add(enemy);
        }
    }


    public void Update(Vector2 playerCoords)
    {
        foreach (var enemy in Enemies)
            enemy.Update(playerCoords);

        Enemies.RemoveAll(enemy => enemy.CurrentHealth <= 0);
    }

    public void Draw()
    {
        foreach (var enemy in Enemies)
        {
            if (Globals.DebugFlag)
                if (!_map.Map.IsInFov((int)enemy.Coords.X, (int)enemy.Coords.Y))
                    continue;
            enemy.Draw();
        }
    }
}