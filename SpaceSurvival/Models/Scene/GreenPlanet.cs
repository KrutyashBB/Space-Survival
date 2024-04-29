namespace SpaceSurvival;

public class GreenPlanet : PlanetScene
{
    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("BasicGreen");

    private readonly Rectangle[] _walkableTexturesSource =
    {
        new(16, 32, 16, 16),
        new(32, 32, 16, 16),
        new(16, 48, 16, 16),
    };

    private readonly Rectangle[] _notWalkableTexturesSource =
    {
        new(32, 0, 16, 16),
        new(112, 32, 16, 16),
    };

    public GreenPlanet(GameManager gameManager) : base(gameManager)
    {
        TileSetForMap = _tileSetForMap;
        WalkableTexturesSource = _walkableTexturesSource;
        NotWalkableTexturesSource = _notWalkableTexturesSource;
    }

    public override void Activate()
    {
        base.Activate();
        
        LootManager = new LootManager(Map, Scale);
        LootManager.AddLoot(LootType.Type1, 1);
        LootManager.AddLoot(LootType.Type2, 1);
        LootManager.AddLoot(LootType.Type3, 1);

        EnemyManager = new EnemyManager(Player, Map, Scale);
        EnemyManager.CreateEnemies(10);

        CombatManager.Init(Player, EnemyManager.GetEnemies());
    }
}