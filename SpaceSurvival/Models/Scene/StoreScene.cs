namespace SpaceSurvival;

public class StoreScene : Scene
{
    private PurchasePanel _purchasePanel;
    private PlayerInventoryPanel _playerInventoryPanel;
    private ShipStoragePanel _shipStoragePanel;
    private const int DistanceBetweenPanels = 50;

    protected override void Load()
    {
        DragDropManager.Init();
        _purchasePanel = new PurchasePanel(Globals.Content.Load<Texture2D>("Big_Blue_Panel"), Vector2.Zero, 0.38f);
        _shipStoragePanel =
            new ShipStoragePanel(Globals.Content.Load<Texture2D>("Big_Blue_Panel"), Vector2.Zero, 0.38f);
        _playerInventoryPanel = new PlayerInventoryPanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            Vector2.Zero, 0.35f);
    }

    public override void Activate()
    {
        Load();
        _shipStoragePanel.FillCellsWithLoot();
    }

    public override void Update()
    {
        var spacing = (Globals.WindowSize.X - _purchasePanel.Size.X - _shipStoragePanel.Size.X) / 3;
        _purchasePanel.Update(spacing, 10);
        _shipStoragePanel.Update(spacing + _purchasePanel.Size.X + spacing, 10);
        _playerInventoryPanel.Update(_shipStoragePanel.Position.X + _shipStoragePanel.Size.X / 2f - _playerInventoryPanel.Size.X / 2f,
            _shipStoragePanel.Position.Y + _shipStoragePanel.Size.Y + DistanceBetweenPanels);
    }

    protected override void Draw()
    {
        _purchasePanel.Draw();
        _shipStoragePanel.Draw();
        _playerInventoryPanel.Draw();
        _playerInventoryPanel.DrawLoot();
        _shipStoragePanel.DrawLoot();
    }
}