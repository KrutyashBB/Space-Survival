using System.Collections.Generic;

namespace SpaceSurvival;

public class SmallBluePanel : Sprite
{
    public readonly List<CellSmallBluePanel> _cells = new();
    public static readonly int CountCells = InventoryManager.CapacityPlayerInventory;

    private readonly Texture2D _cellTexture = Globals.Content.Load<Texture2D>("Small_Orange_Cell");

    public SmallBluePanel(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        for (var i = 0; i < CountCells; i++)
            _cells.Add(new CellSmallBluePanel(_cellTexture, new Vector2(0, 0), Scale));
    }


    private void UpdateCellsPosition()
    {
        var totalWidth = _cellTexture.Width * Scale * CountCells;
        var spacing = (Size.X - totalWidth) / (CountCells + 1);
        var startX = Position.X + spacing;

        for (var i = 0; i < _cells.Count; i++)
            _cells[i].Position = new Vector2(startX + (_cellTexture.Width * Scale + spacing) * i,
                Position.Y + (Size.Y - _cellTexture.Height * Scale) / 2f);
    }

    private static Vector2 CalculatePosition(Vector2 playerPos, Point sizePanel, Point mapSize)
    {
        var dx = playerPos.X - sizePanel.X / 2f + 40;
        dx = MathHelper.Clamp(dx, Globals.WindowSize.X / 2f - sizePanel.X / 2f,
            mapSize.X - Globals.WindowSize.X / 2f - sizePanel.X / 2f);
        var dy = playerPos.Y + Globals.WindowSize.Y / 2f - sizePanel.Y;
        dy = MathHelper.Clamp(dy, Globals.WindowSize.Y - sizePanel.Y, mapSize.Y - sizePanel.Y);
        return new Vector2(dx, dy);
    }

    public void Update(Vector2 playerPos, Point mapSize)
    {
        Position = CalculatePosition(playerPos, Size, mapSize);
        UpdateCellsPosition();
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var cell in _cells)
            cell.Draw();
    }
}