namespace SpaceSurvival;

public class Unit : Sprite
{
    public int CurrentHealth { get; set; }
    protected int MaxHealth { get; init; }
    public int Damage { get; protected init; }

    protected Unit(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
    }
}