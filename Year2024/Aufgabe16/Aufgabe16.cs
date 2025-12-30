using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe16 : IAufgabe
{
    private readonly char[,] _map;
    private readonly SortedDictionary<Point, Dictionary<Direction, int>> _length = [];
    private readonly Queue<Point> _pointsToCheck = [];
    private readonly List<int> _rotateCost = [RotateCost, RotateCost * 2, RotateCost];
    private readonly Point _endPoint;

    private const int RotateCost = 1000;

    public Aufgabe16()
    {
        var input = Utilities.ReadInput(2024, 16);
        _map = new char[input.Length, input[0].Length];
        Point startPoint = new();

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                _map[y, x] = input[y][x];

                if (_map[y, x] == 'S')
                {
                    startPoint = new(x, y);
                }
                else if (_map[y, x] == 'E')
                {
                    _endPoint = new(x, y);
                }
            }
        }

        _length.Add(startPoint, []);
        _length[startPoint].Add(Direction.Right, 0);
        AddRotate(startPoint);
        AddPointsToCheck(startPoint, 0);
    }

    public string Calc()
    {
        while (_pointsToCheck.Count > 0)
        {
            var point = _pointsToCheck.Dequeue();
            (int costForPoint, Direction direction) = GetCostForPoint(point);
            _length.TryAdd(point, []);
            if (_length[point].TryGetValue(direction, out int value))
            {
                costForPoint = Math.Min(value, costForPoint);
                _length[point][direction] = Math.Min(value, costForPoint);
            }
            else
            {
                _length[point].Add(direction, costForPoint);
            }
            AddRotate(point);
            AddPointsToCheck(point, costForPoint);
        }

        return _length[_endPoint].Values.Min().ToString();
    }

    private void AddPointsToCheck(Point point, int cost)
    {
        AddPointsToCheckInner(point.X + 1, point.Y, cost);
        AddPointsToCheckInner(point.X - 1, point.Y, cost);
        AddPointsToCheckInner(point.X, point.Y + 1, cost);
        AddPointsToCheckInner(point.X, point.Y - 1, cost);
    }

    private void AddPointsToCheckInner(int x, int y, int cost)
    {
        var charAtPosition = _map[y, x];

        if (charAtPosition == '#')
        {
            return;
        }

        Point point = new(x, y);
        if (_length.TryGetValue(point, out Dictionary<Direction, int>? values))
        {
            var min = values.Values.Min();
            if (min < cost + RotateCost)
            {
                return;
            }
        }

        if (_pointsToCheck.Contains(point))
        {
            return;
        }

        _pointsToCheck.Enqueue(point);
    }

    private (int, Direction) GetCostForPoint(Point point)
    {
        int cost = int.MaxValue, costInner;
        Direction direction = Direction.Right;

        costInner = GetCostForPointInner(new(point.X + 1, point.Y), Direction.Left);
        if (costInner < cost)
        {
            cost = costInner;
            direction = Direction.Left;
        }

        costInner = GetCostForPointInner(new(point.X - 1, point.Y), Direction.Right);
        if (costInner < cost)
        {
            cost = costInner;
            direction = Direction.Right;
        }

        costInner = GetCostForPointInner(new(point.X, point.Y + 1), Direction.Up);
        if (costInner < cost)
        {
            cost = costInner;
            direction = Direction.Up;
        }

        costInner = GetCostForPointInner(new(point.X, point.Y - 1), Direction.Down);
        if (costInner < cost)
        {
            cost = costInner;
            direction = Direction.Down;
        }

        return (cost + 1, direction);
    }

    private int GetCostForPointInner(Point point, Direction direction)
    {
        if (_length.TryGetValue(point, out var directionMap))
        {
            return directionMap[direction];
        }

        return int.MaxValue;
    }

    private void AddRotate(Point point)
    {
        Direction direction = _length[point].Keys.First();

        for (int i = 0; i < 3; i++)
        {
            var costAtDirection = _length[point][direction];
            direction = GetNextDirection(direction);
            AddRotateInner(point, direction, costAtDirection);
        }
    }

    private void AddRotateInner(Point point, Direction direction, int costAtDirection)
    {
        for (int i = 0; i < _rotateCost.Count; i++)
        {
            if (_length[point].TryGetValue(direction, out int value))
            {
                _length[point][direction] = Math.Min(value, costAtDirection + _rotateCost[i]);
            }
            else
            {
                _length[point][direction] = costAtDirection + _rotateCost[i];
            }

            direction = GetNextDirection(direction);
        }
    }

    private static Direction GetNextDirection(Direction direction)
    {
        int intValue = (int)direction;

        intValue++;

        intValue %= 4;

        return (Direction)intValue;
    }

    private enum Direction
    {
        Left = 0,
        Down = 1,
        Right = 2,
        Up = 3,
    }

    private struct Point : IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int CompareTo(Point other)
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

        public override readonly string ToString()
        {
            return $"X={X},Y={Y}";
        }
    }
}
