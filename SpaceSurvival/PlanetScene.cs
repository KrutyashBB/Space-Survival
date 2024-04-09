namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private SpaceBackground _background;

    public PlanetScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
        _background = new SpaceBackground(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 3f);
    }

    public override void Activate()
    {
        GameManager.Ship.Position = new Vector2(10, 10);
        GameManager.Ship.SetBounds(_background.Size);
    }

    public override void Update()
    {
        GameManager.Ship.Update();
        CalculateTranslation(GameManager.Ship, _background);
    }

    protected override void Draw()
    {
        _background.Draw();
        GameManager.Ship.Draw();
    }
}