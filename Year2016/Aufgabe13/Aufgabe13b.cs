using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe13b : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe13b()
    {
        const int GridSize = 60;

        var input = Utilities.ReadInputAsInt(2016, 13);
        _grid = new(GridSize);
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                var number = x * x + 3 * x + 2 * x * y + y + y * y;
                number += input;
                _grid.SetValue(x, y, number.ToString("B").Count(x => x == '1') % 2 == 0 ? ' ' : '#');

            }
        }
    }

    public string Calc()
    {
        Point startPoint = new(1, 1);
        HashSet<Point> visited = [];
        HashSet<Point> currentPoints = [startPoint];

        for (int i = 0; i <= 50; i++)
        {
            HashSet<Point> nextCurrentPoints = new(currentPoints.Count);

            foreach (var point in currentPoints)
            {
                if (_grid.GetValue(point) == ' ')
                {
                    visited.Add(point);
                    nextCurrentPoints.AddRange(_grid.GetInBoundNeighbours(point).Where(x => !visited.Contains(x) && !nextCurrentPoints.Contains(x)));
                }
            }

            currentPoints = nextCurrentPoints;
        }

        return visited.Count.ToString();
    }
}