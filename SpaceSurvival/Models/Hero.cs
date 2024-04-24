using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class Hero
{
    private const float Speed = 200f;
    private const float Scale = 4;
    private const int FrameX = 4;
    private const int FrameY = 8;
    private readonly AnimationManager _anims = new();

    private static Texture2D _heroAnimationTexture;
    private readonly float _heroWidth;
    private readonly float _heroHeight;

    private List<Rectangle> collisions;

    public Vector2 Position { get; private set; } = new(800, 900);

    public Hero()
    {
        _heroAnimationTexture = Globals.Content.Load<Texture2D>("hero");
        _heroWidth = _heroAnimationTexture.Width / FrameX * Scale;
        _heroHeight = _heroAnimationTexture.Height / FrameY * Scale;

        collisions = MapRenderer.GetCollisionRectangles();

        _anims.AddAnimation(new Vector2(0, -1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 1));
        _anims.AddAnimation(new Vector2(-1, -1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 2));
        _anims.AddAnimation(new Vector2(-1, 0), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 3));
        _anims.AddAnimation(new Vector2(-1, 1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 4));

        _anims.AddAnimation(new Vector2(0, 1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 5));
        _anims.AddAnimation(new Vector2(1, 1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 6));
        _anims.AddAnimation(new Vector2(1, 0), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 7));
        _anims.AddAnimation(new Vector2(1, -1), new Animation(_heroAnimationTexture, FrameX, FrameY, 0.1f, Scale, 8));
    }

    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new Rectangle((int)pos.X, (int)pos.Y, (int)_heroWidth, (int)_heroHeight);
    }

    public void Update()
    {
        foreach (var obj in collisions)
        {
            if (obj.Intersects(CalculateBounds(Position)))
                Console.WriteLine(555555);
        }
        

        var directionHero = new Vector2(InputManager.Direction.X, -InputManager.Direction.Y);
        if (InputManager.Moving)
            Position += Vector2.Normalize(directionHero) * Speed * Globals.TotalSeconds;
        
        _anims.Update(InputManager.Direction);
    }

    public void Draw()
    {
        _anims.Draw(Position);
    }
}