using System;

namespace SpaceSurvival;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;

    private const int MinWidth = 1200;
    private const int MinHeight = 900;

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
        _graphics.PreferredBackBufferWidth = MinWidth;
        _graphics.PreferredBackBufferHeight = MinHeight;
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
        if (_graphics.PreferredBackBufferWidth < MinWidth)
            _graphics.PreferredBackBufferWidth = MinWidth;
        if (_graphics.PreferredBackBufferHeight < MinHeight)
            _graphics.PreferredBackBufferHeight = MinHeight;
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