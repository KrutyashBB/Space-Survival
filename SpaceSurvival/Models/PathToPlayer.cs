namespace SpaceSurvival;

public class PathToPlayer
{
    private readonly IMap _map;
    private readonly PathFinder _pathFinder;
    private Path _cells;

    public PathToPlayer(IMap map)
    {
        _map = map;
        _pathFinder = new PathFinder(map);
    }

    public Cell StepForward =>
        (Cell)_cells.TryStepForward();

    public bool IsNearThePlayer =>
        _cells.Length == 2;

    public void CreateFromTo(int fromX, int fromY, int toX, int toY)
    {
        _cells = _pathFinder.TryFindShortestPath(_map.GetCell(fromX, fromY),
            _map.GetCell(toX, toY));
    }
}