using System;

namespace SpaceSurvival;

public class ProgressBar
{
    private readonly Texture2D _background;
    private readonly Texture2D _foreground;
    private Vector2 _position;
    private readonly float _maxValue;
    private float _currentValue;
    private Rectangle _part;
    private readonly float _scale;

    public ProgressBar(int maxValue, Vector2 position, float scale)
    {
        _background = Globals.Content.Load<Texture2D>("back");
        _foreground = Globals.Content.Load<Texture2D>("front");
        _maxValue = maxValue;
        _currentValue = maxValue;
        _position = position;
        _scale = scale;
        _part = new Rectangle(0, 0, _foreground.Width, _foreground.Height);
    }

    public void Update(int value, Vector2 pos)
    {
        _position = Vector2.Lerp(_position, pos, 0.3f);
        _currentValue = value;
        _part.Width = (int)(_currentValue / _maxValue * _foreground.Width);
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(_background, _position, null, Color.White, 0f, Vector2.Zero, _scale,
            SpriteEffects.None, 0f);
        Globals.SpriteBatch.Draw(_foreground, _position, _part, Color.White, 0f, Vector2.Zero, _scale,
            SpriteEffects.None, 0f);
    }
}