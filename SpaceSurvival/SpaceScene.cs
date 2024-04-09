namespace SpaceSurvival;

public class SpaceScene : Scene
{
    private SpaceBackground _background;

    public SpaceScene(GameManager gameManager) : base(gameManager)
    {
        
    }

    protected override void Load()
    {
        _background = new SpaceBackground(Globals.Content.Load<Texture2D>("bg-space"), new Vector2(0, 0), 3f);
        PlanetManager.Init(_background.Size.X, _background.Size.Y);
        PlanetManager.CreatePlanets();
    }

    public override void Activate()
    {
        GameManager.Ship.Position = new Vector2(_background.Size.X / 2f, _background.Size.Y / 2f);
        GameManager.Ship.SetBounds(_background.Size);
    }


    public override void Update()
    {
        GameManager.Ship.Update();
        PlanetManager.Update();
        CalculateTranslation(GameManager.Ship, _background);
    }

    protected override void Draw()
    {
        _background.Draw();
        PlanetManager.Draw();
        GameManager.Ship.Draw();
    }
}