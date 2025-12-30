using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe10b : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe10b()
    {
        _grid = Grid.CreateCharGrid(2019, 10);
    }

    public string Calc()
    {
        var point = GetPoint();
        DictionaryList<Point, Point> points = [];

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (point.Y == y && point.X == x)
                {
                    continue;
                }

                if (_grid.GetValue(x, y) != '#')
                {
                    continue;
                }

                int yDifference = y - point.Y;
                int xDifference = x - point.X;
                int gcd = MathEnhancement.GetHighestCommonDivisor(xDifference, yDifference);

                points.Add(new(xDifference / gcd, yDifference / gcd), new Point(x, y));
            }
        }

        var clockwisePoints = GetClockwise(points.Keys);

        int asteroidsDestroyed = 0;
        int currentCount = 1;
        do
        {
            foreach (var clockwisePoint in clockwisePoints)
            {
                if (points[clockwisePoint].Count >= currentCount)
                {
                    asteroidsDestroyed++;

                    if (asteroidsDestroyed == 200)
                    {
                        var destroyedPoint = points[clockwisePoint].OrderBy(x => x.GetManhattenDistance(point)).ElementAt(currentCount - 1);
                        return (destroyedPoint.X * 100 + destroyedPoint.Y).ToString();
                    }
                }
            }

            currentCount++;
        } while (true);

        throw new NotImplementedException();
    }

    private Point GetPoint()
    {
        int maxAsteroids = 0;
        Point currentPoint = new();

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

                if (currentAsteroinds > maxAsteroids)
                {
                    maxAsteroids = currentAsteroinds;
                    currentPoint = new(x, y);
                }
            }
        }

        return currentPoint;
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

    private static List<Point> GetClockwise(IEnumerable<Point> points)
    {
        return [.. points.OrderByDescending(x => Math.Atan2(x.X, x.Y))];
    }
}
