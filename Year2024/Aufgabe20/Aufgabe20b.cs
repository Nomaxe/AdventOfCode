using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2024;

internal class Aufgabe20b : IAufgabe
{
    private readonly Grid<char> _grid;
    private readonly CompleteSolver _solver;
    private readonly Point _startPoint;

    private readonly HashSet<(Point Start, Point End)> _cheatingAmount = [];
    private readonly Dictionary<int, int> _cheatingAmountCount = [];

    public Aufgabe20b()
    {
        _grid = Grid.CreateCharGrid(2024, 20);
        _solver = new CompleteSolver(_grid);
        _startPoint = _grid.GetPointOfValue('S');
    }

    public string Calc()
    {
        _solver.SolveLabyrinth(_startPoint);

        foreach (var pathPoint in _solver.GetWayPoints())
        {
            for (int x = 0; x <= 20; x++)
            {
                for (int y = 0; y <= 20 - x; y++)
                {
                    //2 Stunden verschwendet, da Aufgabe anders verstanden
                    //Punkte werden doppelt geprüft, ist mir jetzt aber egal :)
                    var length = _solver.GetLength(pathPoint);
                    CheckForCheating(pathPoint, length, new Point(pathPoint.X + x, pathPoint.Y + y), x + y);
                    CheckForCheating(pathPoint, length, new Point(pathPoint.X + x, pathPoint.Y - y), x + y);
                    CheckForCheating(pathPoint, length, new Point(pathPoint.X - x, pathPoint.Y + y), x + y);
                    CheckForCheating(pathPoint, length, new Point(pathPoint.X - x, pathPoint.Y - y), x + y);
                }
            }
        }

        return _cheatingAmount.Count.ToString();
    }

    private void CheckForCheating(Point startPoint, int startPointLength, Point endPoint, int pathLength)
    {
        if (!_grid.IsInBounds(endPoint))
        {
            return;
        }

        if (_solver.ContainsLength(endPoint))
        {
            var pointLength = _solver.GetLength(endPoint);
            if (pointLength > startPointLength)
            {
                var cheatingAmount = pointLength - startPointLength - pathLength;

                if (cheatingAmount >= 100)
                {
                    if (_cheatingAmount.Add((startPoint, endPoint)))
                    {
                        if (_cheatingAmountCount.TryGetValue(cheatingAmount, out int value))
                        {
                            _cheatingAmountCount[cheatingAmount] = ++value;
                        }
                        else
                        {
                            _cheatingAmountCount.Add(cheatingAmount, 1);
                        }
                    }
                }
            }
        }
    }
}
