using System.Collections.Generic;

namespace SpaceSurvival;

public class SalesPanel : Sprite
{
    private List<CellInventoryPanel> Cells { get; } = new();
    private readonly Texture2D _cellTexture = Globals.Content.Load<Texture2D>("Small_White_Cell");
    private const float CellScale = 0.60f;
    private const int CountCells = 9;

    public SalesPanel(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        for (var i = 0; i < CountCells; i++)
            Cells.Add(new CellInventoryPanel(CellOwner.Sale, _cellTexture, new Vector2(0, 0), CellScale));
    }

    private void UpdateCellsPosition()
    {
        var countColumns = 3;
        var spacingX = (Size.X - _cellTexture.Width * CellScale * countColumns) / 4;
        var startX = Position.X + spacingX;

        var x = 0;
        var y = 0;

        foreach (var cell in Cells)
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

    public void FillCellsWithLoot()
    {
        for (var i = 0; i < CountCells; i++)
            if (InventoryManager.ShipInventory.Count > i)
                Cells[i].Loot = InventoryManager.ShipInventory[i];
    }

    private void UpdateCellState()
    {
        foreach (var cell in Cells)
            cell.Update();
    }

    public void Update(float xPos, float offsetFromTopScreen)
    {
        Position = new Vector2(xPos, offsetFromTopScreen);

        UpdateCellsPosition();
        UpdateCellState();
    }

    public void DrawLoot()
    {
        foreach (var cell in Cells)
            cell.Loot?.Draw();
    }

    public override void Draw()
    {
        base.Draw();
        foreach (var cell in Cells)
            cell.Draw();
    }
}