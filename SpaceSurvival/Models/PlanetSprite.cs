using System;

namespace SpaceSurvival;

public class PlanetSprite : Sprite
{
    public int Id { get; private set; }
    private TypePlanet TypePlanet { get; }
    public Rectangle Rect { get; private set; }
    
    private float _rotation;
    private readonly float _randomSpeed;
    private const float RotationSpeed = 0.3f;
    
    public bool IsCollisionWithPlayerShip = false;
    private readonly SpriteFont _font;

    public PlanetSprite(int id, TypePlanet typePlanet, Texture2D tex, Vector2 pos, int scale) : base(tex, pos, scale)
    {
        Id = id;
        TypePlanet = typePlanet;
        Scale = scale;

        var random = new Random();
        _randomSpeed = (float)random.NextDouble();

        Rect = new Rectangle((int)pos.X - Size.X / 2, (int)pos.Y - Size.Y / 2, Size.X, Size.Y);

        _font = Globals.Content.Load<SpriteFont>("font");
    }

    public void Update()
    {
        _rotation += _randomSpeed * RotationSpeed * Globals.TotalSeconds;
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None,
            1f);
        
        if (TypePlanet == TypePlanet.Store)
            Globals.SpriteBatch.DrawString(_font, "STORE", new Vector2(Position.X - Size.X * 0.2f, Position.Y - 30),
                Color.Blue);
        
        if (IsCollisionWithPlayerShip)
            Globals.SpriteBatch.DrawString(_font, "Click F",
                new Vector2(Position.X - 50, Position.Y - Size.Y * 0.7f), Color.White);
    }
}