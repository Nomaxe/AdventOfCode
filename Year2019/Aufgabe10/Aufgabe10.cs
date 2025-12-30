using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe10 : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe10()
    {
        _grid = Grid.CreateCharGrid(2019, 10);
    }

    public string Calc()
    {
        int maxAsteroids = 0;

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) != '#')
                {
                    continue;
                }

                int currentAsteroinds = 0;

                if (CheckLeft(x, y))
                {
                    currentAsteroinds++;
                }
                if (CheckRight(x, y))
                {
                    currentAsteroinds++;
                }

                currentAsteroinds += CheckTop(x, y);
                currentAsteroinds += CheckBottom(x, y);

                maxAsteroids = int.Max(maxAsteroids, currentAsteroinds);
            }
        }

        return maxAsteroids.ToString();
    }

    private bool CheckLeft(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            if (_grid.GetValue(i, y) == '#')
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckRight(int x, int y)
    {
        for (int i = _grid.SizeX - 1; i > x; i--)
        {
            if (_grid.GetValue(i, y) == '#')
            {
                return true;
            }
        }

        return false;
    }

    private int CheckTop(int xCheck, int yCheck)
    {
        HashSet<Point> points = [];

        for (int y = 0; y < yCheck; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) != '#')
                {
                    continue;
                }

                int yDifference = yCheck - y;
                int xDifference = x - xCheck;
                int gcd = MathEnhancement.GetHighestCommonDivisor(xDifference, yDifference);

                points.Add(new(xDifference / gcd, yDifference / gcd));
            }
        }

        return points.Count;
    }

    private int CheckBottom(int xCheck, int yCheck)
    {
        HashSet<Point> points = [];

        for (int y = yCheck + 1; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) != '#')
                {
                    continue;
                }

                int yDifference = y - yCheck;
                int xDifference = x - xCheck;
                int gcd = MathEnhancement.GetHighestCommonDivisor(xDifference, yDifference);

                points.Add(new(xDifference / gcd, yDifference / gcd));
            }
        }

        return points.Count;
    }
}
