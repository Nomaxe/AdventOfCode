using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2023;

internal class Aufgabe10 : IAufgabe
{
    private readonly Grid _grid;
    private Direction _direction;

    public Aufgabe10()
    {
        _grid = Grid.CreateCharGrid(2023, 10);
    }

    public string Calc()
    {
        Point startPoint = _grid.GetPointOfValue('S');
        List<Point> points = [startPoint];

        var currentPoint = GetNextStartPoint(startPoint);

        while (currentPoint != startPoint)
        {
            points.Add(currentPoint);
            currentPoint = GetNextPoint(currentPoint);
        }

        return (points.Count / 2).ToString();
    }

    private Point GetNextStartPoint(Point point)
    {
        var pipe = _grid.GetValue(point.X, point.Y - 1);
        if (pipe == '|' || pipe == '7' || pipe == 'F')
        {
            _direction = Direction.Up;
            return new(point.X, point.Y - 1);
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

        pipe = _grid.GetValue(point.X + 1, point.Y);
        if (pipe == '-' || pipe == 'J' || pipe == 'F')
        {
            _direction = Direction.Right;
            return new(point.X + 1, point.Y);
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
