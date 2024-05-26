using System.Collections.Generic;

namespace SpaceSurvival;

public static class ProjectileManager
{
    private static Texture2D _enemyBulletTexture;
    private static Texture2D _playerBulletTexture;
    public static List<Projectile> Projectiles { get; private set; }

    public static void Init()
    {
        Projectiles = new List<Projectile>();
        _enemyBulletTexture = Globals.Content.Load<Texture2D>("redBullet");
        _playerBulletTexture = Globals.Content.Load<Texture2D>("blueBullet");
    }

    public static void AddEnemyProjectile(ProjectileData data)
    {
        Projectiles.Add(new Projectile(_enemyBulletTexture, data, 1f));
    }

    public static void AddPlayerProjectile(ProjectileData data)
    {
        Projectiles.Add(new Projectile(_playerBulletTexture, data, 1f));
    }

    public static void Update()
    {
        foreach (var p in Projectiles)
            p.Update();
        Projectiles.RemoveAll(p => p.LifeSpan <= 0);
    }

    public static void Draw()
    {
        foreach (var p in Projectiles)
            p.Draw();
    }
}