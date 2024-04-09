namespace SpaceSurvival;

public abstract class Scene
{
    protected readonly RenderTarget2D Target;
    protected readonly GameManager GameManager;
    private Matrix _translation;

    protected Scene(GameManager gameManager)
    {
        GameManager = gameManager;
        Target = Globals.GetNewRenderTarget();
        Load();
    }

    protected abstract void Load();
    protected abstract void Draw();
    public abstract void Update();
    public abstract void Activate();
    
    public void CalculateTranslation(Sprite target, Sprite screen)
    {
        var dx = target.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, screen.Size.X - Globals.WindowSize.X);
        var dy = target.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, screen.Size.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0);
    }

    public virtual RenderTarget2D GetFrame()
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