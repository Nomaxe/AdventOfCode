using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe21 : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe21()
    {
        _grid = Grid.CreateCharGrid(2023, 21);
    }

    public string Calc()
    {
        HashSet<Point> points = [_grid.GetPointOfValue('S')];

        for (int i = 0; i < 64; i++)
        {
            HashSet<Point> nextPoints = [];

            foreach (var point in points)
            {
                foreach (var neighbour in point.GetNeighbours())
                {
                    if (_grid.GetValue(neighbour) != '#')
                    {
                        nextPoints.Add(neighbour);
                    }
                }
            }

            points = nextPoints;
        }

        return points.Count.ToString();
    }
}
