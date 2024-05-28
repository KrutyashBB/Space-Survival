namespace SpaceSurvival;

public static class InputManager
{
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    public static KeyboardState KeyboardState { get; private set; }
    private static KeyboardState LastKeyboard { get; set; }
    private static MouseState MouseState { get; set; }
    private static MouseState LastMouseState { get; set; }


    public static bool KeyboardKeyPressed(Keys key)
    {
        return KeyboardState.IsKeyDown(key) && LastKeyboard.IsKeyUp(key);
    }

    public static bool ClickedMouseLeftButton()
    {
        return MouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
    }

    public static bool ReleasedMouseLeftButton()
    {
        return MouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed;
    }

    public static void Update()
    {
        LastKeyboard = KeyboardState;
        KeyboardState = Keyboard.GetState();

        _direction = Vector2.Zero;
        if (KeyboardState.IsKeyDown(Keys.W))
            _direction.Y++;
        if (KeyboardState.IsKeyDown(Keys.S))
            _direction.Y--;
        if (KeyboardState.IsKeyDown(Keys.A))
            _direction.X--;
        if (KeyboardState.IsKeyDown(Keys.D))
            _direction.X++;

        LastMouseState = MouseState;
        MouseState = Mouse.GetState();
    }
}