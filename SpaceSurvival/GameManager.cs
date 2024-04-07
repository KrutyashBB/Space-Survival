namespace SpaceSurvival;

public class GameManager
{
    private readonly Ship _ship = new(Globals.Content.Load<Texture2D>("tiny_ship8"), new(200, 200));

    public void Update()
    {
        InputManager.Update();
        _ship.Update();
    }

    public void Draw()
    {
        _ship.Draw();
    }
}