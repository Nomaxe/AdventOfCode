using System.Runtime.InteropServices;

namespace AdventOfCode.Utils.Labyrinth;

internal class CompleteSolver
{
    private readonly Grid _grid;
    private readonly Grid<int> _length;
    private readonly HashSet<char> _wall;
    private readonly HashSet<Point> _nextPoints = [];

    public CompleteSolver(Grid<char> grid)
    {
        _grid = grid;
        _length = new Grid<int>(grid.SizeX, grid.SizeY);
        _wall = ['#'];
    }

    public void SolveLabyrinth(Point startPoint)
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

    public void AddWallCharacter(char character)
    {
        _wall.Add(character);
    }

    public bool ContainsLength(Point point)
    {
        return _length.GetValue(point) > 0;
    }

    public int GetLength(Point point)
    {
        return _length.GetValue(point);
    }

    public int GetMaxLength()
    {
        return _length.Max();
    }

    public bool TryGetLength(Point point, out int length)
    {
        var value = _length.GetValue(point);
        if (value > 0)
        {
            length = value;
            return true;
        }

        length = -1;
        return false;
    }

    public List<Point> GetFirstStepTo(Point point)
    {
        if (!TryGetLength(point, out int value))
        {
            throw new NotImplementedException($"{point} not reachable");
        }

        return GetFirstStepTo(GetNeighboursWithLength(point, value - 1).ToList(), value - 1);
    }

    public IEnumerable<Point> GetWayPoints()
    {
        for (int y = 0; y < _length.SizeY; y++)
        {
            for (int x = 0; x < _length.SizeX; x++)
            {
                if (_length.GetValue(x, y) > 0)
                {
                    yield return new(x, y);
                }
            }
        }
    }

    private bool AddLength(Point point, int length)
    {
        var value = _length.GetValue(point);

        if (value == 0 || length < value)
        {
            _length.SetValue(point, length);
            return true;
        }

        return false;
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
}
