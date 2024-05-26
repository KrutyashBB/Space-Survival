using System.Collections.Generic;

namespace SpaceSurvival;

public static class DragDropManager
{
    private static List<CellInventoryPanel> _draggables;
    private static List<CellInventoryPanel> _targets;
    private static CellInventoryPanel _dragItem;

    public static void Init()
    {
        _draggables = new List<CellInventoryPanel>();
        _targets = new List<CellInventoryPanel>();
        _dragItem = null;
    }

    public static void AddDraggable(CellInventoryPanel draggable)
    {
        _draggables.Add(draggable);
    }

    public static void AddTarget(CellInventoryPanel item)
    {
        _targets.Add(item);
    }

    private static void CheckDragStart()
    {
        if (InputManager.ClickedMouseLeftButton())
        {
            foreach (var item in _draggables)
            {
                if (item.Rect.Contains(InputManager.MousePosition))
                {
                    _dragItem = item;
                    break;
                }
            }
        }
    }

    private static void MovingLootToPlayerInventory()
    {
        if (InventoryManager.CapacityPlayerInventory > 0)
        {
            InventoryManager.AddToPlayerInventory(_dragItem.Loot);
            InventoryManager.RemoveFromShipInventory(_dragItem.Loot);
        }
    }

    private static void MovingLootToShipInventory()
    {
        if (InventoryManager.CapacityShipInventory > 0)
        {
            InventoryManager.AddToShipInventory(_dragItem.Loot);
            InventoryManager.RemoveFromPlayerInventory(_dragItem.Loot);
        }
    }

    private static void CheckTarget()
    {
        foreach (var item in _targets)
        {
            if (item.Rect.Contains(InputManager.MousePosition) && item.Loot == null)
            {
                if (_dragItem.Owner == CellOwner.PlayerInventory && item.Owner == CellOwner.ShipStorage)
                    MovingLootToShipInventory();
                else if (_dragItem.Owner == CellOwner.ShipStorage && item.Owner == CellOwner.PlayerInventory)
                    MovingLootToPlayerInventory();

                item.Loot = _dragItem.Loot;
                _dragItem.Loot = null;
                break;
            }
        }
    }

    private static void CheckDragStop()
    {
        if (InputManager.ReleasedMouseLeftButton())
        {
            CheckTarget();
            _dragItem = null;
        }
    }

    public static void Update()
    {
        CheckDragStart();

        if (_dragItem?.Loot != null)
        {
            _dragItem.Loot.Position = InputManager.MousePosition - _dragItem.Loot.Origin;
            CheckDragStop();
        }
    }
}