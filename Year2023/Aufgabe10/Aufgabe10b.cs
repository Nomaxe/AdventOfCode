using AdventOfCode.Utils;
using System.Runtime.InteropServices;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2023;

internal class Aufgabe10b : IAufgabe
{
    private readonly Grid _grid;
    private Direction _direction;
    private Dictionary<int, SortedList<int, bool>> _loop;

    public Aufgabe10b()
    {
        _grid = Grid.CreateCharGrid(2023, 10);
        _loop = [];
    }

    public string Calc()
    {
        Point startPoint = _grid.GetPointOfValue('S');
        HashSet<Point> points = [startPoint];
        int count = 0;

        var currentPoint = GetNextStartPoint(startPoint);

        while (currentPoint != startPoint)
        {
            points.Add(currentPoint);
            var nextPoint = GetNextPoint(currentPoint);

            if (currentPoint.Y != nextPoint.Y)
            {
                int x = int.Min(currentPoint.X, nextPoint.X);
                int y = int.Max(currentPoint.Y, nextPoint.Y);

                ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_loop, y, out _);
                list ??= [];
                list.Add(x, _direction == Direction.Down);
            }

            currentPoint = nextPoint;
        }

        for (int y = 0; y < _grid.SizeY; y++)
        {
            if (!_loop.TryGetValue(y, out var list))
            {
                continue;
            }

            for (int x = 0; x < _grid.SizeX; x++)
            {
                Point point = new(x, y);
                if (points.Contains(point))
                {
                    continue;
                }

                if (list.Where(l => l.Key > x).FirstOrDefault().Value)
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }

    private Point GetNextStartPoint(Point point)
    {
        var pipe = _grid.GetValue(point.X + 1, point.Y);
        if (pipe == '-' || pipe == 'J' || pipe == 'F')
        {
            _direction = Direction.Right;
            return new(point.X + 1, point.Y);
        }

        pipe = _grid.GetValue(point.X, point.Y + 1);
        if (pipe == '|' || pipe == 'L' || pipe == 'J')
        {
            _direction = Direction.Down;
            return new(point.X, point.Y + 1);
        }

        pipe = _grid.GetValue(point.X - 1, point.Y);
        if (pipe == '-' || pipe == 'L' || pipe == '7')
        {
            _direction = Direction.Left;
            return new(point.X - 1, point.Y);
        }

        throw new NotImplementedException();
    }

    private Point GetNextPoint(Point point)
    {
        var pipe = _grid.GetValue(point);

        switch (pipe)
        {
            case '|':
                if (_direction == Direction.Up)
                {
                    return new(point.X, point.Y - 1);
                }
                else
                {
                    return new(point.X, point.Y + 1);
                }
            case '-':
                if (_direction == Direction.Right)
                {
                    return new(point.X + 1, point.Y);
                }
                else
                {
                    return new(point.X - 1, point.Y);
                }
            case 'L':
                if (_direction == Direction.Down)
                {
                    _direction = Direction.Right;
                    return new(point.X + 1, point.Y);
                }
                else
                {
                    _direction = Direction.Up;
                    return new(point.X, point.Y - 1);
                }
            case 'J':
                if (_direction == Direction.Down)
                {
                    _direction = Direction.Left;
                    return new(point.X - 1, point.Y);
                }
                else
                {
                    _direction = Direction.Up;
                    return new(point.X, point.Y - 1);
                }
            case '7':
                if (_direction == Direction.Up)
                {
                    _direction = Direction.Left;
                    return new(point.X - 1, point.Y);
                }
                else
                {
                    _direction = Direction.Down;
                    return new(point.X, point.Y + 1);
                }
            case 'F':
                if (_direction == Direction.Up)
                {
                    _direction = Direction.Right;
                    return new(point.X + 1, point.Y);
                }
                else
                {
                    _direction = Direction.Down;
                    return new(point.X, point.Y + 1);
                }
            default:
                throw new NotImplementedException();
        }
    }
}
