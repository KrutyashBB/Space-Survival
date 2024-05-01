using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class SpaceScene : Scene
{
    private Ship _ship;

    private readonly List<EnemyShip> _enemies = new();
    private FollowMovementEnemyShip _followMovement;

    private Sprite _background;
    private Matrix _translation;

    public SpaceScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        _background = new Sprite(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 4f);
        _ship = new Ship(Globals.Content.Load<Texture2D>("tiny_ship8"),
            new Vector2(_background.Size.X / 2f, _background.Size.Y / 2f), 1f);

        _followMovement = new FollowMovementEnemyShip { Target = _ship };

        for (var i = 0; i < 5; i++)
            _enemies.Add(new EnemyShip(Globals.Content.Load<Texture2D>("tiny_ship8"), new(800 * i, 50), 1f)
                { MoveEnemy = new PatrolMovementEnemyShip(10, _background.Size) });
        
        ProjectileManager.Init();
        
        PlanetManager.Init(_background.Size.X, _background.Size.Y);
        PlanetManager.CreatePlanets();
    }

    public override void Activate()
    {
        _ship.SetBounds(_background.Size);
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
                planet.IsCollision = true;
                if (InputManager.KeyPressed(Keys.Tab))
                    LoadPlanetScene(planet);
            }
            else
                planet.IsCollision = false;
        }
    }

    private void CheckCollisionWithBullets()
    {
        foreach (var bullet in ProjectileManager.Projectiles)
        {
            if (_ship.Rect.Intersects(bullet.Rect))
                Console.WriteLine("Hit");
        }
    }

    private static void LoadPlanetScene(PlanetSprite planet)
    {
        if (planet.Type == TypePlanet.Green)
            SceneManager.SwitchScene(Scenes.GreenPlanet);
        if (planet.Type == TypePlanet.Red)
            SceneManager.SwitchScene(Scenes.RedPlanet);
        if (planet.Type == TypePlanet.Ice)
            SceneManager.SwitchScene(Scenes.IcePlanet);
        if (planet.Type == TypePlanet.Violet)
            SceneManager.SwitchScene(Scenes.VioletPlanet);
    }

    public override void Update()
    {
        _ship.Update();

        foreach (var enemy in _enemies)
        {
            var dir = _ship.Position - enemy.Position;
            if (dir.Length() < 500 && enemy.MoveEnemy is PatrolMovementEnemyShip)
                enemy.MoveEnemy = _followMovement;
            else if (dir.Length() > 500 && enemy.MoveEnemy is FollowMovementEnemyShip)
                enemy.MoveEnemy = new PatrolMovementEnemyShip(10, _background.Size);
            enemy.Update();
        }

        ProjectileManager.Update();
        CheckCollisionWithBullets();
        PlanetManager.Update();
        CalculateTranslation(_ship, _background);
        CheckCollisionWithPlanet();
    }

    protected override void Draw()
    {
        _background.Draw();
        PlanetManager.Draw();
        ProjectileManager.Draw();
        foreach (var enemy in _enemies)
            enemy.Draw();
        _ship.Draw();
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