using System;

namespace SpaceSurvival;

public enum TypeFireTrail
{
    Ordinary,
    Enlarged
}

public class FireTrail
{
    private readonly AnimationManager _anims = new();
    private Vector2 _position;
    private readonly Point _size;
    private readonly Vector2 _origin;

    public FireTrail(Texture2D tex, float scale)
    {
        _size = new Point((int)(tex.Width / 4f * scale), (int)(tex.Height / 2f * scale));
        _origin = new Vector2(_size.X / 2f, _size.Y / 2f);

        _anims.AddAnimation(TypeFireTrail.Ordinary, new Animation(tex, 4, 2, 0.1f));
        _anims.AddAnimation(TypeFireTrail.Enlarged, new Animation(tex, 4, 2, 0.1f, 2));
    }

    public void Update(Vector2 pos, float rotation, bool isEnlarged)
    {
        var xOffset = (float)Math.Sin(rotation) * 80f;
        var yOffset = -(float)Math.Cos(rotation) * 80f;
        _position = pos - new Vector2(xOffset, yOffset);

        _anims.Update(isEnlarged ? TypeFireTrail.Enlarged : TypeFireTrail.Ordinary);
    }

    public void Draw(float rotation)
    {
        _anims.Draw(new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), rotation, _origin);
    }
}