namespace SpaceSurvival;

public class Unit : Sprite
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public string Name { get; set; }
    
    public Unit(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
    {
    }
}