namespace SpaceSurvival;

public class SpaceScene : Scene
{
    private Sprite _background;
    private Matrix _translation;

    public SpaceScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        _background = new Sprite(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 3f);
        PlanetManager.Init(_background.Size.X, _background.Size.Y);
        PlanetManager.CreatePlanets();
    }

    public override void Activate()
    {
        GameManager.Ship.Position = new Vector2(_background.Size.X / 2f, _background.Size.Y / 2f);
        GameManager.Ship.SetBounds(_background.Size);
    }

    public void CalculateTranslation(Sprite target, Sprite screen)
    {
        var dx = target.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, screen.Size.X - Globals.WindowSize.X);
        var dy = target.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, screen.Size.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0);
    }


    public override void Update()
    {
        GameManager.Ship.Update();
        PlanetManager.Update();
        CalculateTranslation(GameManager.Ship, _background);
    }

    protected override void Draw()
    {
        _background.Draw();
        PlanetManager.Draw();
        GameManager.Ship.Draw();
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