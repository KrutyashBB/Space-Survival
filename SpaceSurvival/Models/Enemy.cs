using System;

namespace SpaceSurvival;

public class Enemy : Unit
{
    public Vector2 Coords;
    private Vector2 _newPosition;

    private readonly IMap _map;
    private const int MapCellTexSize = 16;
    private readonly PathToPlayer _path;

    private AnimationManager _anims = new();
    private Vector2 _direction;

    private readonly ProgressBar _healthBar;

    private float _movementTimer;
    private const float MovementDelay = 0.8f;


    public Enemy(Texture2D tex, Vector2 pos, float scale, int framesX, PathToPlayer path, IMap map) :
        base(tex, pos, scale)
    {
        _path = path;
        _map = map;

        Coords = pos;
        Size = new Point((int)(MapCellTexSize * Scale), (int)(MapCellTexSize * Scale));
        Position = new Vector2(Coords.X * MapCellTexSize * scale, Coords.Y * MapCellTexSize * scale);
        _newPosition = Position;

        _anims.AddAnimation("Idle", new Animation(Texture, framesX, 6, 0.1f));
        _anims.AddAnimation("RightRun", new Animation(Texture, framesX, 6, 0.1f, 3));
        _anims.AddAnimation("LeftRun", new Animation(Texture, framesX, 6, 0.1f, 4));
        _anims.AddAnimation("RightAttack", new Animation(Texture, framesX, 6, 0.21f, 5));
        _anims.AddAnimation("LeftAttack", new Animation(Texture, framesX, 6, 0.21f, 6));

        CurrentHealth = MaxHealth = 14;
        Damage = 5;
        Name = "Enemy";

        _healthBar = new ProgressBar(MaxHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f),
            0.2f);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Update(Vector2 playerCoords)
    {
        if (!_map.IsInFov((int)Coords.X, (int)Coords.Y))
        {
            _anims.Update("Idle");
            return;
        }

        _movementTimer += Globals.TotalSeconds;

        _healthBar.Update(CurrentHealth, new Vector2(Position.X - Size.X * 0.3f, Position.Y - Size.Y / 2f));

        if (_movementTimer > MovementDelay)
        {
            _path.CreateFromTO((int)Coords.X, (int)Coords.Y, (int)playerCoords.X, (int)playerCoords.Y);

            if (!_path.IsNearThePlayer)
            {
                var cell = _path.StepForward;
                if (cell != null)
                {
                    _direction = new Vector2(cell.X - Coords.X, Math.Abs(cell.Y - Coords.Y));
                    Coords.X = cell.X;
                    Coords.Y = cell.Y;
                    _newPosition = new Vector2(Coords.X * MapCellTexSize * Scale, Coords.Y * MapCellTexSize * Scale);
                }
            }

            _movementTimer = 0;
        }

        if (_path.IsNearThePlayer)
        {
            if (_direction == new Vector2(1, 0) || _direction == new Vector2(0, 1))
                _anims.Update("RightAttack");
            else if (_direction == new Vector2(-1, 0))
                _anims.Update("LeftAttack");
        }
        else
        {
            if (_direction == new Vector2(1, 0) || _direction == new Vector2(0, 1))
                _anims.Update("RightRun");
            else if (_direction == new Vector2(-1, 0))
                _anims.Update("LeftRun");
        }

        Position = Vector2.Lerp(Position, _newPosition, 0.1f);
    }

    public override void Draw()
    {
        _anims.Draw(new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y), Rotation, Vector2.Zero);
        _healthBar.Draw();
    }
}