using System;

namespace SpaceSurvival;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;

    private int minWidth = 1200;
    private int minHeight = 900;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Globals.WindowSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        _graphics.PreferredBackBufferWidth = minWidth;
        _graphics.PreferredBackBufferHeight = minHeight;
        Window.AllowUserResizing = true;
        _graphics.ApplyChanges();

        Globals.GraphicsDevice = GraphicsDevice;
        Globals.Content = Content;

        _gameManager = new GameManager();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
    }

    protected override void Update(GameTime gameTime)
    {
        if (_graphics.PreferredBackBufferWidth < minWidth)
            _graphics.PreferredBackBufferWidth = minWidth;
        if (_graphics.PreferredBackBufferHeight < minHeight)
            _graphics.PreferredBackBufferHeight = minHeight;
        _graphics.ApplyChanges();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.Update(gameTime);
        _gameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _gameManager.Draw();

        base.Draw(gameTime);
    }
}