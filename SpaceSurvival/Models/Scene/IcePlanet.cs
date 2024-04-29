using System;

namespace SpaceSurvival;

public class IcePlanet : PlanetScene
{
    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("IceSet");

    private readonly Rectangle[] _walkableTexturesSource =
    {
        new(48, 64, 16, 16),
    };

    private readonly Rectangle[] _notWalkableTexturesSource =
    {
        new(0, 0, 16, 16),
        new(16, 0, 16, 16),
        new(32, 0, 16, 16),
        new(0, 48, 16, 16),
        new(64, 32, 16, 16),
        new(16, 48, 16, 16),
    };

    public IcePlanet(GameManager gameManager) : base(gameManager)
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