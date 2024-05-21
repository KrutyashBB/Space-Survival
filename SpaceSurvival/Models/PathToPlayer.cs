
namespace SpaceSurvival;

public class PathToPlayer
{
    // private readonly Vector2 _playerCoords;
    private readonly IMap _map;
    private readonly Texture2D _sprite;
    private readonly PathFinder _pathFinder;
    private Path _cells;

    private readonly int _scale;

    public PathToPlayer(Vector2 playerCoords, IMap map, Texture2D sprite, int scale)
    {
        // _playerCoords = playerCoords;
        _map = map;
        _sprite = sprite;
        _pathFinder = new PathFinder(map);
        _scale = scale;
    }

    public Cell StepForward => (Cell)_cells.TryStepForward();
    public bool IsNearThePlayer => _cells.Length == 2;

    public void CreateFromTO(int fromX, int fromY, int toX, int toY)
    {
        _cells = _pathFinder.TryFindShortestPath(_map.GetCell(fromX, fromY),
            _map.GetCell(toX, toY));
    }

    public void Draw()
    {
        foreach (var cell in _cells.Steps)
        {
            Globals.SpriteBatch.Draw(_sprite,
                new Vector2(cell.X * _sprite.Width * _scale, cell.Y * _sprite.Height * _scale),
                null, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}