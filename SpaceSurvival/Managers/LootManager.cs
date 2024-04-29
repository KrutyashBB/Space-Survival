using System.Collections.Generic;

namespace SpaceSurvival;

public enum LootType
{
    Type1,
    Type2,
    Type3
}

public class LootManager
{
    public readonly List<Loot> Loots = new();
    private readonly MapGenerate _map;
    private readonly int _scale;

    public LootManager(MapGenerate map, int scale)
    {
        _map = map;
        _scale = scale;
    }

    public void AddLoot(LootType type, int count)
    {
        var color = Color.White;
        var value = 0;
        switch (type)
        {
            case LootType.Type1:
                color = Color.White;
                value = 1;
                break;
            case LootType.Type2:
                color = Color.Red;
                value = 2;
                break;
            case LootType.Type3:
                color = Color.Green;
                value = 3;
                break;
        }

        for (var i = 0; i < count; i++)
            Loots.Add(new Loot(type, value, Globals.Content.Load<Texture2D>("path"),
                _map.GetRandomEmptyCell(), color, _scale));
    }

    public void Draw()
    {
        foreach (var loot in Loots)
            loot.Draw();
    }
}