using Microsoft.Xna.Framework.Content;

namespace SpaceSurvival;

public static class Globals
{
    public static float TotalSeconds { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }
    public static Point WindowSize { get; set; }
    public static bool DebugFlag { get; set; } = false;

    public static RenderTarget2D GetNewRenderTarget()
    {
        return new RenderTarget2D(GraphicsDevice, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
    }

    public static void Update(GameTime gameTime)
    {
        WindowSize = GraphicsDevice.Viewport.Bounds.Size;
        TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}