namespace AdventOfCode.Utils.Labyrinth;

internal class CompleteSolver : LabyrinthSolver
{
    protected readonly HashSet<Point> _nextPoints = [];

    public CompleteSolver(Grid<char> grid) : base(grid)
    {

    }

    public override void SolveLabyrinth(Point startPoint)
    {
        int length = 0;

        AddPoint(startPoint, length);
        length++;

        while (_nextPoints.Count > 0)
        {
            var pointsToCheck = _nextPoints.ToList();
            _nextPoints.Clear();

            foreach (var point in pointsToCheck)
            {
                AddPoint(point, length);
            }

            length++;
        }
    }

    public List<Point> GetFirstStepTo(Point point)
    {
        if (!TryGetLength(point, out int value))
        {
            throw new NotImplementedException($"{point} not reachable");
        }

        return GetFirstStepTo(GetNeighboursWithLength(point, value - 1).ToList(), value - 1);
    }

    private List<Point> GetFirstStepTo(List<Point> points, int length)
    {
        if (length == 1)
        {
            return points;
        }

        List<Point> newPoints = new();
        foreach (var point in points)
        {
            newPoints.AddRange(GetNeighboursWithLength(point, length - 1));
        }

        return GetFirstStepTo(newPoints, length - 1);
    }

    private IEnumerable<Point> GetNeighboursWithLength(Point point, int length)
    {
        foreach (var neighbour in _grid.GetInBoundNeighbours(point))
        {
            if (TryGetLength(neighbour, out int value))
            {
                if (value == length)
                {
                    yield return neighbour;
                }
            }
        }
    }

    protected virtual IEnumerable<Point> GetNeighbours(Point point)
    {
        return point.GetNeighbours();
    }

    private void AddPoint(Point point, int length)
    {
        var result = AddLength(point, length);

        if (result)
        {
            foreach (var neighbour in GetNeighbours(point))
            {
                TryAddPoint(neighbour.X, neighbour.Y);
            }
        }
    }

    private void TryAddPoint(int x, int y)
    {
        if (!_grid.IsInBounds(x, y))
        {
            return;
        }

        if (_wall.Contains(_grid.GetValue(x, y)))
        {
            return;
        }

        Point point = new(x, y);
        if (ContainsLength(point))
        {
            return;
        }

        _nextPoints.Add(point);
    }
}
