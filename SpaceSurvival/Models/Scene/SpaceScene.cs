namespace SpaceSurvival;

public class SpaceScene : Scene
{
    private Ship ship;

    private Sprite _background;
    private Matrix _translation;

    public SpaceScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        _background = new Sprite(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 4f);
        ship = new Ship(Globals.Content.Load<Texture2D>("tiny_ship8"),
            new Vector2(_background.Size.X / 2f, _background.Size.Y / 2f), 1f);
        PlanetManager.Init(_background.Size.X, _background.Size.Y);
        PlanetManager.CreatePlanets();
    }

    public override void Activate()
    {
        ship.SetBounds(_background.Size);
    }

    public void CalculateTranslation(Sprite target, Sprite screen)
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
            if (ship.Rect.Intersects(planet.Rect))
            {
                planet.IsCollision = true;
                if (InputManager.KeyPressed(Keys.Tab))
                    LoadPlanetScene(planet);
            }
            else
                planet.IsCollision = false;
        }
    }

    private void LoadPlanetScene(PlanetSprite planet)
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
        ship.Update();
        PlanetManager.Update();
        CalculateTranslation(ship, _background);
        CheckCollisionWithPlanet();
    }

    protected override void Draw()
    {
        _background.Draw();
        PlanetManager.Draw();
        ship.Draw();
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