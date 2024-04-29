using System;

namespace SpaceSurvival;

public class VioletPlanet : PlanetScene
{
    private readonly Texture2D _tileSetForMap = Globals.Content.Load<Texture2D>("DarkCastle");

    private readonly Rectangle[] _walkableTexturesSource =
    {
        new(32, 64, 16, 16),
        new(48, 64, 16, 16),
        new(48, 48, 16, 16),
    };

    private readonly Rectangle[] _notWalkableTexturesSource =
    {
        new(0, 0, 16, 16),
        new(64, 48, 16, 16),
    };

    public VioletPlanet(GameManager gameManager) : base(gameManager)
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
        EnemyManager.CreateEnemies(4);

        CombatManager.Init(Player, EnemyManager.GetEnemies());
    }
}