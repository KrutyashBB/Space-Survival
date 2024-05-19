using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SpaceSurvival;

public class BigBluePanel : Sprite
{
    private readonly List<CellInventoryPanel> _cells = new();
    private static readonly int CountCells = InventoryManager.CapacityShipInventory;

    private readonly Texture2D _cellTexture = Globals.Content.Load<Texture2D>("Small_Orange_Cell");

    private const float CellScale = 0.60f;

    public BigBluePanel(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Position = new Vector2(Globals.WindowSize.X / 2f - Size.X / 2f, 0);
        for (var i = 0; i < CountCells; i++)
            _cells.Add(new CellInventoryPanel(CellOwner.ShipInventory, _cellTexture, new Vector2(0, 0), CellScale));
    }

    private void UpdateCellsPosition()
    {
        var spacingX = (Size.X - _cellTexture.Width * CellScale * 3) / 4;
        var startX = Position.X + spacingX;

        var x = 0;
        var y = 0;

        foreach (var cell in _cells)
        {
            cell.Position = new Vector2(startX + (_cellTexture.Width * CellScale + spacingX) * x,
                Position.Y + 20 + (_cellTexture.Height * CellScale + 20) * y);
            x++;
            if (x == 3)
            {
                x = 0;
                y++;
            }
        }
    }

    private void FillCellsWithLoot()
    {
        for (var i = 0; i < CountCells; i++)
            if (InventoryManager.ShipInventory.Count > i)
                _cells[i].Loot = InventoryManager.ShipInventory[i];
    }

    private void UpdateCellState()
    {
        foreach (var cell in _cells)
            cell.Update();
    }

    public void Update(float offsetFromTopScreen)
    {
        Position = new Vector2(Globals.WindowSize.X / 2f - Size.X / 2f, offsetFromTopScreen);

        UpdateCellsPosition();
        FillCellsWithLoot();
        UpdateCellState();
    }

    public override void Draw()
    {
        base.Draw();
        foreach (var cell in _cells)
            cell.Draw();
    }
}