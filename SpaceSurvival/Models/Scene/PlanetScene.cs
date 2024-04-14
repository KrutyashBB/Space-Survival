namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private readonly MapRenderer _map = new();
    private readonly Hero _hero = new();
    private Matrix _translation;


    public PlanetScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
    }

    public override void Activate()
    {
    }

    public override void Update()
    {
        _hero.Update();
        CalculateTranslation(_hero.Position);
    }

    public void CalculateTranslation(Vector2 target)
    {
        var dx = target.X - Globals.WindowSize.X / 2f;
        var dy = target.Y - Globals.WindowSize.Y / 2f;
        _translation = Matrix.CreateTranslation(-dx, -dy, 0);
    }

    protected override void Draw()
    {
        _map.Draw();
        _hero.Draw();
    }

    public override RenderTarget2D GetFrame()
    {
        Globals.GraphicsDevice.SetRenderTarget(Target);
        Globals.GraphicsDevice.Clear(Color.Black);

        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _translation);
        Draw();
        Globals.SpriteBatch.End();

        Globals.GraphicsDevice.SetRenderTarget(null);
        return Target;
    }
}