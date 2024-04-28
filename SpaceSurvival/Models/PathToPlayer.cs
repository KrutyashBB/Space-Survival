
namespace SpaceSurvival;

public class PathToPlayer
{
    private readonly HeroForMapGenerator _player;
    private readonly IMap _map;
    private readonly Texture2D _sprite;
    private readonly PathFinder _pathFinder;
    private Path _cells;

    private int Scale;

    public PathToPlayer(HeroForMapGenerator player, IMap map, Texture2D sprite, int scale)
    {
        _player = player;
        _map = map;
        _sprite = sprite;
        _pathFinder = new PathFinder(map);
        Scale = scale;
    }

    public Cell StepForward => (Cell)_cells.TryStepForward();
    public bool IsNearThePlayer => _cells.Length == 2;

    public void CreateFrom(int x, int y)
    {
        _cells = _pathFinder.ShortestPath(_map.GetCell(x, y),
            _map.GetCell((int)_player.Coords.X, (int)_player.Coords.Y));
    }

    public void Draw()
    {
        foreach (var cell in _cells.Steps)
        {
            Globals.SpriteBatch.Draw(_sprite,
                new Vector2(cell.X * _sprite.Width * Scale, cell.Y * _sprite.Height * Scale),
                null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}