namespace SpaceSurvival;

public class CellSmallBluePanel : Sprite
{
    public Loot Loot;

    public CellSmallBluePanel(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
    }

    public void Update()
    {
        if (Loot != null)
            Loot.Position = new Vector2(Position.X + (Size.X - Loot.Size.X) / 2f,
                Position.Y + (Size.Y - Loot.Size.Y) / 2f);
    }

    public override void Draw()
    {
        base.Draw();
        Loot?.Draw();
    }
}