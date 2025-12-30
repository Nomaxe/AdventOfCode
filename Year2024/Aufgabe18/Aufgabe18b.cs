using AdventOfCode.Utils;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2024;

internal class Aufgabe18b : IAufgabe
{
    private const int MapSize = 71;
    private readonly string[] _input;
    private readonly char[,] _map;
    private readonly HashSet<Point> _length = [];

    private readonly HashSet<Point> _nextPoints = [];

    public Aufgabe18b()
    {
        _input = Utilities.ReadInput(2024, 18);
        _map = new char[MapSize, MapSize];
    }

    public string Calc()
    {
        bool result;
        int corruptedBlocks = 1500;

        do
        {
            corruptedBlocks++;
            result = Test(corruptedBlocks);
        } while (result);

        return _input[corruptedBlocks];
    }

    private bool Test(int corruptedBlocks)
    {
        _length.Clear();

        for (int x = 0; x < MapSize; x++)
        {
            for (int y = 0; y < MapSize; y++)
            {
                _map[y, x] = '.';
            }
        }

        foreach (var line in _input.Take(corruptedBlocks + 1))
        {
            var split = line.Split(',');
            _map[int.Parse(split[1]), int.Parse(split[0])] = '#';
        }

        AddPoint(0, 0, 0);

        int length = 1;
        while (_nextPoints.Count > 0)
        {
            var pointsToCheck = _nextPoints.ToList();
            _nextPoints.Clear();

            foreach (var point in pointsToCheck)
            {
                AddPoint(point.X, point.Y, length);
            }

            length++;
        }

        return _length.Contains(new(MapSize - 1, MapSize - 1));
    }

    private void AddPoint(int x, int y, int length)
    {
        var result = _length.Add(new(x, y, length));

        if (result)
        {
            TryAddPoint(x + 1, y);
            TryAddPoint(x - 1, y);
            TryAddPoint(x, y + 1);
            TryAddPoint(x, y - 1);
        }
    }

    private void TryAddPoint(int x, int y)
    {
        if (x < 0 || x >= MapSize)
        {
            return;
        }

        if (y < 0 || y >= MapSize)
        {
            return;
        }

        if (_map[y, x] == '#')
        {
            return;
        }

        Point point = new(x, y);
        if (_length.Contains(point))
        {
            return;
        }

        _nextPoints.Add(point);
    }

    private struct Point : IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int x, int y, int length) : this(x, y)
        {
            Length = length;
        }

        public readonly int CompareTo(Point other)
        {
            var compare = X.CompareTo(other.X);
            if (compare != 0)
            {
                return compare;
            }

            compare = Y.CompareTo(other.Y);
            if (compare != 0)
            {
                return compare;
            }

            return 0;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Point other)
            {
                return X == other.X && Y == other.Y;
            }

            return false;
        }

        public override readonly string ToString()
        {
            return $"X={X},Y={Y}";
        }
    }
}
