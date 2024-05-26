using System.Collections.Generic;
using System.Linq;

namespace SpaceSurvival;

public class PlayerInventoryPanel : Sprite
{
    private List<CellInventoryPanel> Cells { get; } = new();
    private static readonly int CountCells = InventoryManager.CapacityPlayerInventory;
    private readonly Texture2D _cellTexture = Globals.Content.Load<Texture2D>("Small_Orange_Cell");

    public PlayerInventoryPanel(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        for (var i = 0; i < CountCells; i++)
            Cells.Add(new CellInventoryPanel(CellOwner.PlayerInventory, _cellTexture, new Vector2(0, 0), Scale));

        foreach (var loot in InventoryManager.PlayerInventory)
        {
            var cell = Cells.FirstOrDefault(c => c.Loot == null);
            if (cell != null)
                cell.Loot = loot;
        }
    }


    private void UpdateCellsPosition()
    {
        var totalWidth = _cellTexture.Width * Scale * CountCells;
        var spacing = (Size.X - totalWidth) / (CountCells + 1);
        var startX = Position.X + spacing;

        for (var i = 0; i < Cells.Count; i++)
            Cells[i].Position = new Vector2(startX + (_cellTexture.Width * Scale + spacing) * i,
                Position.Y + (Size.Y - _cellTexture.Height * Scale) / 2f);
    }

    private static Vector2 CalculatePosition(Vector2 playerPos, Point playerSize, Point sizePanel, Point mapSize)
    {
        var dx = playerPos.X - sizePanel.X / 2f + playerSize.X / 2f;
        dx = MathHelper.Clamp(dx, Globals.WindowSize.X / 2f - sizePanel.X / 2f,
            mapSize.X - Globals.WindowSize.X / 2f - sizePanel.X / 2f);
        var dy = playerPos.Y + Globals.WindowSize.Y / 2f - sizePanel.Y;
        dy = MathHelper.Clamp(dy, Globals.WindowSize.Y - sizePanel.Y, mapSize.Y - sizePanel.Y);
        return new Vector2(dx, dy);
    }


    public void FillCellWithLoot()
    {
        var cell = Cells.FirstOrDefault(c => c.Loot == null);
        if (cell != null)
            cell.Loot = InventoryManager.PlayerInventory.LastOrDefault();
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

    public void Update(Vector2 playerPos, Point playerSize, Point mapSize)
    {
        Position = Vector2.Lerp(Position, CalculatePosition(playerPos, playerSize, Size, mapSize), 0.5f);

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