using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class SpaceScene : Scene
{
    private Ship _ship;

    private EnemyShipManager _enemyShipManager;

    private Sprite _background;
    private InventoryBtn _inventoryBtn;

    private Matrix _translation;

    protected override void Load()
    {
        _background = new Sprite(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 4f);
        _inventoryBtn = new InventoryBtn(Globals.Content.Load<Texture2D>("Small_Orange_Cell"), Vector2.Zero, 0.5f);

        _ship = new Ship(Globals.Content.Load<Texture2D>("tiny_ship8"),
            new Vector2(_background.Size.X / 2f, _background.Size.Y / 2f), 1f);
        _ship.SetBounds(_background.Size);

        InventoryManager.Init();

        FollowMovementEnemyShip.Target = _ship;
        PatrolMovementEnemyShip.Range = _background.Size;

        _enemyShipManager = new EnemyShipManager(10, _background.Size);

        ProjectileManager.Init();

        PlanetManager.Init(_background.Size.X, _background.Size.Y);
        PlanetManager.CreatePlanets();
    }

    public override void Activate()
    {
    }

    private void CalculateTranslation(Sprite target, Sprite screen)
    {
        var dx = target.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, screen.Size.X - Globals.WindowSize.X);
        var dy = target.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, screen.Size.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0);
    }

    private void CheckCollisionWithPlanet()
    {
        foreach (var planet in PlanetManager.Planets)
        {
            if (_ship.Rect.Intersects(planet.Rect))
            {
                planet.IsCollisionWithPlayerShip = true;
                if (InputManager.KeyboardKeyPressed(Keys.W))
                    LoadScene(planet);
            }
            else
                planet.IsCollisionWithPlayerShip = false;
        }
    }

    private readonly List<Projectile> _bulletsToRemove = new();

    private void CheckCollisionBulletWithUnit()
    {
        foreach (var bullet in ProjectileManager.Projectiles)
        {
            HitPlayer(bullet);
            HitEnemyShip(bullet);
        }

        foreach (var bullet in _bulletsToRemove)
            ProjectileManager.Projectiles.Remove(bullet);
    }

    private void HitPlayer(Projectile bullet)
    {
        if (_ship.Rect.Intersects(bullet.Rect) && bullet.Type == ProjectileType.EnemyBullet)
        {
            _bulletsToRemove.Add(bullet);
            _ship.CurrentHealth -= bullet.Damage;
        }
    }

    private void HitEnemyShip(Projectile bullet)
    {
        foreach (var enemyShip in _enemyShipManager.EnemyShips)
        {
            if (enemyShip.Rect.Intersects(bullet.Rect) && bullet.Type == ProjectileType.PlayerBullet)
            {
                _bulletsToRemove.Add(bullet);
                enemyShip.CurrentHealth -= bullet.Damage;
            }
        }
    }


    private static void LoadScene(PlanetSprite planet)
    {
        SceneManager.SwitchScene(planet.Id);
    }

    public override void Update()
    {
        if (InputManager.KeyboardKeyPressed(Keys.Tab))
            SceneManager.SwitchScene((int)TypeScene.PlayerShipScene);

        _ship.Update();
        _inventoryBtn.Update(_ship.Position, _background.Size);
        _enemyShipManager.Update(_ship);
        ProjectileManager.Update();
        CheckCollisionBulletWithUnit();
        PlanetManager.Update();
        CalculateTranslation(_ship, _background);
        CheckCollisionWithPlanet();
    }

    protected override void Draw()
    {
        _background.Draw();
        PlanetManager.Draw();
        ProjectileManager.Draw();
        _enemyShipManager.Draw();
        _ship.Draw();
        _inventoryBtn.Draw();
    }

    public override RenderTarget2D GetFrame()
    {
        Globals.GraphicsDevice.SetRenderTarget(Target);
        Globals.GraphicsDevice.Clear(Color.Black);

        Globals.SpriteBatch.Begin(transformMatrix: _translation);
        Draw();
        Globals.SpriteBatch.End();

        Globals.GraphicsDevice.SetRenderTarget(null);
        return Target;
    }
}