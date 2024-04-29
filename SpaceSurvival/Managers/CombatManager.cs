using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceSurvival;

public static class CombatManager
{
    private static HeroForMapGenerator _hero;
    private static List<Enemy> _enemies;

    public static void Init(HeroForMapGenerator hero, List<Enemy> enemies)
    {
        _hero = hero;
        _enemies = enemies;
    }

    public static void Attack(Unit attacker, Unit defender)
    {
        var damage = attacker.Damage;
        defender.Health -= damage;
        Console.WriteLine($"{attacker.Name} attacks {defender.Name} for {damage} damage! Health {defender.Health}");

        if (defender.Health <= 0)
        {
            if (defender is Enemy enemy)
                _enemies.Remove(enemy);

            Console.WriteLine($"{attacker.Name} killed {defender.Name}");
        }
    }

    public static Unit UnitAt(int x, int y)
    {
        if (IsHeroAt(x, y))
            return _hero;
        return EnemyAt(x, y);
    }

    public static bool IsHeroAt(int x, int y) =>
        (int)_hero.Coords.X == x && (int)_hero.Coords.Y == y;

    public static Enemy EnemyAt(int x, int y) =>
        _enemies.FirstOrDefault(enemy => (int)enemy.Coords.X == x && (int)enemy.Coords.Y == y);

    public static bool IsEnemyAt(int x, int y) =>
        EnemyAt(x, y) != null;
}