using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2024;

internal class Aufgabe20 : IAufgabe
{
    private readonly Grid<char> _grid;
    private readonly ILabyrinthSolver _solver;
    private readonly Point _startPoint;

    private ulong _result = 0;

    public Aufgabe20()
    {
        _grid = Grid.CreateCharGrid(2024, 20);
        _solver = new CompleteSolver(_grid);
        _startPoint = _grid.GetPointOfValue('S');
    }

    public string Calc()
    {
        _solver.SolveLabyrinth(_startPoint);

        foreach (var point in _solver)
        {
            var length = _solver.GetLength(point);
            CheckForCheating(point, length, point.X + 1, point.Y, point.X + 2, point.Y);
            CheckForCheating(point, length, point.X - 1, point.Y, point.X - 2, point.Y);
            CheckForCheating(point, length, point.X, point.Y + 1, point.X, point.Y + 2);
            CheckForCheating(point, length, point.X, point.Y - 1, point.X, point.Y - 2);
        }

        return _result.ToString();
    }

    private void CheckForCheating(Point orignalPoint, int orginalPointLength, int xWall, int yWall, int xPath, int yPath)
    {
        if (!_grid.IsInBounds(xPath, yPath))
        {
            return;
        }


        if (_grid.GetValue(xWall, yWall) != '#')
        {
            return;
        }

        Point point = new(xPath, yPath);

        if (_solver.TryGetLength(point, out var length))
        {
            if (length > orginalPointLength)
            {
                if (length - orginalPointLength - 2 >= 100)
                {
                    _result++;
                }
            }
        }
    }
}
