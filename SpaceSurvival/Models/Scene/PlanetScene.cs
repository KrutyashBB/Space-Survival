namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private Map _map = new();
    private Hero _hero = new();


    public PlanetScene(GameManager gameManager) : base(gameManager)
    {
    }

    protected override void Load()
    {
    }

    public override void Activate()
    {
    }

    public override void Update()
    {
        _hero.Update();
    }

    protected override void Draw()
    {
        _map.Draw();
        _hero.Draw();
    }
}