namespace SpaceSurvival;

public static class InputManager
{
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static bool Moving => _direction != Vector2.Zero;
    private static KeyboardState _lastKeyboard;
    private static KeyboardState _currentKeyboard;

    public static KeyboardState KeyboardState { get; private set; }


    public static bool KeyPressed(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key) && _lastKeyboard.IsKeyUp(key);
    }

    public static void Update()
    {
        KeyboardState = Keyboard.GetState();

        _direction = Vector2.Zero;
        if (KeyboardState.IsKeyDown(Keys.Up))
            _direction.Y++;
        if (KeyboardState.IsKeyDown(Keys.Down))
            _direction.Y--;
        if (KeyboardState.IsKeyDown(Keys.Left))
            _direction.X--;
        if (KeyboardState.IsKeyDown(Keys.Right))
            _direction.X++;

        _lastKeyboard = _currentKeyboard;
        _currentKeyboard = Keyboard.GetState();
    }
}