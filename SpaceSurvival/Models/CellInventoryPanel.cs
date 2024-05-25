namespace SpaceSurvival;

public enum CellOwner
{
    PlayerInventory,
    ShipStorage,
    Purchase,
    Sale
}

public class CellInventoryPanel : Sprite
{
    public readonly CellOwner Owner;
    public Rectangle Rect;
    public Loot Loot;

    public CellInventoryPanel(CellOwner owner, Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
        Owner = owner;
        DragDropManager.AddDraggable(this);
        DragDropManager.AddTarget(this);
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
    }

    public void Update()
    {
        Rect = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
        if (Loot != null)
            Loot.Position = new Vector2(Position.X + (Size.X - Loot.Size.X) / 2f,
                Position.Y + (Size.Y - Loot.Size.Y) / 2f);
    }
}