namespace SpaceSurvival;

public class Hero
{
    private Vector2 position = new(100, 100);
    private readonly float _speed = 200f;
    private readonly AnimationManager _anims = new();

    public Hero()
    {
        var heroTexture = Globals.Content.Load<Texture2D>("hero");
        _anims.AddAnimation(new Vector2(0, -1), new Animation(heroTexture, 4, 8, 0.1f, 4, 1));
        _anims.AddAnimation(new Vector2(-1, -1), new Animation(heroTexture, 4, 8, 0.1f, 4, 2));
        _anims.AddAnimation(new Vector2(-1, 0), new Animation(heroTexture, 4, 8, 0.1f, 4, 3));
        _anims.AddAnimation(new Vector2(-1, 1), new Animation(heroTexture, 4, 8, 0.1f, 4, 4));
        
        _anims.AddAnimation(new Vector2(0, 1), new Animation(heroTexture, 4, 8, 0.1f, 4, 5));
        _anims.AddAnimation(new Vector2(1, 1), new Animation(heroTexture, 4, 8, 0.1f, 4, 6));
        _anims.AddAnimation(new Vector2(1, 0), new Animation(heroTexture, 4, 8, 0.1f, 4, 7));
        _anims.AddAnimation(new Vector2(1, -1), new Animation(heroTexture, 4, 8, 0.1f, 4, 8));
    }

    public void Update()
    {
        var directionHero = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
        if (InputManager.Moving)
            position += Vector2.Normalize(directionHero) * _speed * Globals.TotalSeconds;

        _anims.Update(InputManager.Direction);
    }

    public void Draw()
    {
        _anims.Draw(position);
    }
}