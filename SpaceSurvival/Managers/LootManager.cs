using System.Collections.Generic;

namespace SpaceSurvival;

public enum LootType
{
    Gold,
    Ruby,
    Bronze
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
        Texture2D texture = null;
        var value = 0;
        switch (type)
        {
            case LootType.Gold:
                texture = Globals.Content.Load<Texture2D>("ThingsToBuyAndCollect/topaz");
                value = 3;
                break;
            case LootType.Ruby:
                texture = Globals.Content.Load<Texture2D>("ThingsToBuyAndCollect/gemstone");
                value = 2;
                break;
            case LootType.Bronze:
                texture = Globals.Content.Load<Texture2D>("ThingsToBuyAndCollect/bronze");
                value = 1;
                break;
        }

        for (var i = 0; i < count; i++)
            Loots.Add(new Loot(type, value, texture, _map.GetRandomEmptyCell(), _scale));
    }

    public void Draw()
    {
        foreach (var loot in Loots)
        {
            if (!_map.Map.IsInFov((int)loot.Coords.X, (int)loot.Coords.Y))
                continue;
            loot.Draw();
        }
    }
}