namespace SpaceSurvival;

public class PlayerShipScene : Scene
{
    private SmallBluePanel _smallBluePanel;
    private BigBluePanel _bigBluePanel;
    private const int DistanceBetweenPanels = 50;

    private SpacewalkBtn _spacewalkBtn;
    private SpriteFont _font;

    protected override void Load()
    {
        DragDropManager.Init();
        _smallBluePanel = new SmallBluePanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            Vector2.Zero, 0.35f);
        _bigBluePanel = new BigBluePanel(Globals.Content.Load<Texture2D>("Big_Blue_Panel"), Vector2.Zero, 0.38f);

        _spacewalkBtn = new SpacewalkBtn(Globals.Content.Load<Texture2D>("Small_Orange_Cell"), Vector2.Zero, 0.5f);
        _font = Globals.Content.Load<SpriteFont>("font");
    }

    public override void Activate()
    {
        Load();
    }

    public override void Update()
    {
        var offsetFromTopScreen =
            (Globals.WindowSize.Y - _bigBluePanel.Size.Y - _smallBluePanel.Size.Y - DistanceBetweenPanels) / 2f;
        _bigBluePanel.Update(offsetFromTopScreen);
        _smallBluePanel.Update(_bigBluePanel.Position.Y + _bigBluePanel.Size.Y + DistanceBetweenPanels);
        DragDropManager.Update();

        _spacewalkBtn.Update();
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("bg-space"), Vector2.Zero, null, Color.White, 0f,
            Vector2.Zero, 5f, SpriteEffects.None, 0f);
        Globals.SpriteBatch.DrawString(_font, "STORAGE",
            new Vector2(Globals.WindowSize.X / 2f - 150, _bigBluePanel.Position.Y - 70),
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _bigBluePanel.Draw();
        _smallBluePanel.Draw();
        _spacewalkBtn.Draw();
    }
}