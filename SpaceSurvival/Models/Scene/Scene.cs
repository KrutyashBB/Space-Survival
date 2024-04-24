namespace SpaceSurvival;

public abstract class Scene
{
    protected readonly RenderTarget2D Target;
    protected readonly GameManager GameManager;

    protected Scene(GameManager gameManager)
    {
        GameManager = gameManager;
        Target = Globals.GetNewRenderTarget();
        Load();
    }

    protected abstract void Load();
    public abstract void Activate();
    public abstract void Update();
    protected abstract void Draw();

    public virtual RenderTarget2D GetFrame()
    {
        Globals.GraphicsDevice.SetRenderTarget(Target);
        Globals.GraphicsDevice.Clear(Color.Black);

        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Draw();
        Globals.SpriteBatch.End();

        Globals.GraphicsDevice.SetRenderTarget(null);
        return Target;
    }
}