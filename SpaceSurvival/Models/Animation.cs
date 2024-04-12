using System.Collections.Generic;

namespace SpaceSurvival;

public class Animation
{
    private readonly Texture2D _texture;
    private readonly List<Rectangle> _sourceRectangles = new();
    private readonly int _frames;
    private int _frame;
    private readonly float _frameTime;
    private float _frameTimeLeft;
    private float _scale;
    private bool _active = true;

    public Animation(Texture2D texture, int framesX, int framesY, float frameTime, float scale, int row = 1)
    {
        _texture = texture;
        _frameTime = frameTime;
        _frameTimeLeft = frameTime;
        _frames = framesX;
        _scale = scale;
        var frameWidth = texture.Width / framesX;
        var frameHeight = texture.Height / framesY;

        for (var i = 0; i < _frames; i++)
            _sourceRectangles.Add(new Rectangle(frameWidth * i, frameHeight * (row - 1), frameWidth, frameHeight));
    }

    public void Stop()
    {
        _active = false;
    }

    public void Start()
    {
        _active = true;
    }

    public void Reset()
    {
        _frame = 0;
        _frameTimeLeft = _frameTime;
    }

    public void Update()
    {
        if (!_active) return;

        _frameTimeLeft -= Globals.TotalSeconds;

        if (_frameTimeLeft <= 0)
        {
            _frameTimeLeft += _frameTime;
            _frame = (_frame + 1) % _frames;
        }
    }

    public void Draw(Vector2 pos)
    {
        Globals.SpriteBatch.Draw(_texture, pos, _sourceRectangles[_frame], Color.White, 0, Vector2.Zero, _scale,
            SpriteEffects.None, 1);
    }
}