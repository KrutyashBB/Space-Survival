using Microsoft.Xna.Framework.Content;

namespace SpaceSurvival;

public static class Globals
{
    public static float TotalSeconds { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }
    public static Point WindowSize { get; set; }

    public static RenderTarget2D GetNewRenderTarget()
    {
        return new RenderTarget2D(GraphicsDevice, WindowSize.X, WindowSize.Y);
    }

    public static void Update(GameTime gameTime)
    {
        WindowSize = GraphicsDevice.Viewport.Bounds.Size;
        TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}