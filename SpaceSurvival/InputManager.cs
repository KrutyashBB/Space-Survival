﻿namespace SpaceSurvival;

public static class InputManager
{
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;

    public static void Update()
    {
        var keyboardState = Keyboard.GetState();

        _direction = Vector2.Zero;
        if (keyboardState.IsKeyDown(Keys.Up))
            _direction.Y++;
        if (keyboardState.IsKeyDown(Keys.Down))
            _direction.Y--;
        if (keyboardState.IsKeyDown(Keys.Left))
            _direction.X--;
        if (keyboardState.IsKeyDown(Keys.Right))
            _direction.X++;
    }
}