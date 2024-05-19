using System.Collections.Generic;

namespace SpaceSurvival;

public static class InventoryManager
{
    public static readonly List<Loot> PlayerInventory = new();
    public static int CapacityPlayerInventory = 2;
    public static readonly List<Loot> ShipInventory = new();
    public static int CapacityShipInventory = 4;

    public static void AddToPlayerInventory(Loot loot)
    {
        PlayerInventory.Add(loot);
        CapacityPlayerInventory--;
    }

    public static void RemoveFromPlayerInventory(Loot loot)
    {
        PlayerInventory.Remove(loot);
        CapacityPlayerInventory++;
    }

    public static void AddToShipInventory(Loot loot)
    {
        ShipInventory.Add(loot);
        CapacityShipInventory--;
    }

    public static void RemoveFromShipInventory(Loot loot)
    {
        ShipInventory.Remove(loot);
        CapacityShipInventory++;
    }
}