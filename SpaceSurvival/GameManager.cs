using System;

namespace SpaceSurvival;

public class GameManager
{
    private readonly Ship _ship;
    private readonly SpaceBackground _background;
    private Matrix _tranlation;

    public GameManager()
    {
        _background = new(Globals.Content.Load<Texture2D>("bg-space"), new(0, 0));
        _ship = new Ship(Globals.Content.Load<Texture2D>("tiny_ship8"),
            new Vector2(_background.MapSize.X / 2f, _background.MapSize.Y / 2f));
        _ship.SetBounds(_background.MapSize);
    }

    private void CalculateTranslation()
    {
        var dx = _ship.Position.X - Globals.WindowSize.X / 2f;
        dx = MathHelper.Clamp(dx, 0, _background.MapSize.X - Globals.WindowSize.X);
        var dy = _ship.Position.Y - Globals.WindowSize.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, _background.MapSize.Y - Globals.WindowSize.Y);
        _tranlation = Matrix.CreateTranslation(-dx, -dy, 0);
    }

    public void Update()
    {
        InputManager.Update();
        _ship.Update();
        CalculateTranslation();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _tranlation);

        _background.Draw();
        _ship.Draw();

        Globals.SpriteBatch.End();
    }
}