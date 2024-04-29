using System;

namespace SpaceSurvival;

public class RedPlanet : PlanetScene
{
    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("FireSet");

    private readonly Rectangle[] _walkableTexturesSource =
    {
        new(64, 16, 16, 16),
        new(48, 48, 16, 16),
    };

    private readonly Rectangle[] _notWalkableTexturesSource =
    {
        new(48, 0, 16, 16),
        new(48, 16, 16, 16),
        new(48, 32, 16, 16),
        new(64, 32, 16, 16),
    };

    public RedPlanet(GameManager gameManager) : base(gameManager)
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
        EnemyManager.CreateEnemies(7);

        CombatManager.Init(Player, EnemyManager.GetEnemies());
    }
}