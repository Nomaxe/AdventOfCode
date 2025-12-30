using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2025;

internal class Aufgabe09b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Point> _points;
    private readonly DictionaryList<Direction, Line> _lines;

    public Aufgabe09b()
    {
        _input = Utilities.ReadInput(2025, 9);
        _points = new(_input.Length);
        _lines = new();
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _points.Add(new(line));
        }
        AddLines();

        long size = 0;
        for (int i = 0; i < _points.Count; i++)
        {
            for (int j = i + 1; j < _points.Count; j++)
            {
                var point1 = _points[i];
                var point2 = _points[j];

                var checkSize = (long.Max(point1.X, point2.X) - long.Min(point1.X, point2.X) + 1) * (long.Max(point1.Y, point2.Y) - long.Min(point1.Y, point2.Y) + 1);
                if (checkSize < size)
                {
                    continue;
                }

                if (!CheckRectangle(point1, point2))
                {
                    continue;
                }

                size = checkSize;
            }
        }

        return size.ToString();
    }

    private bool CheckRectangle(Point point1, Point point2)
    {
        var xMin = int.Min(point1.X, point2.X);
        var xMax = int.Max(point1.X, point2.X);
        var yMin = int.Min(point1.Y, point2.Y);
        var yMax = int.Max(point1.Y, point2.Y);

        foreach (var line in _lines[Direction.Right])
        {
            if (line.Position <= xMin || line.Position >= xMax)
            {
                continue;
            }

            if ((line.Begin <= yMin && yMin < line.End) || (line.Begin < yMax && yMax <= line.End))
            {
                return false;
            }
        }

        foreach (var line in _lines[Direction.Down])
        {
            if (line.Position <= yMin || line.Position >= yMax)
            {
                continue;
            }

            if ((line.Begin <= xMin && xMin < line.End) || (line.Begin < xMax && xMax <= line.End))
            {
                return false;
            }
        }

        return true;
    }

    private void AddLines()
    {
        var lastPoint = _points[0];

        for (int i = 1; i < _points.Count; i++)
        {
            AddLine(lastPoint, _points[i]);
            lastPoint = _points[i];
        }

        AddLine(lastPoint, _points[0]);
    }

    private void AddLine(Point point1, Point point2)
    {
        if (point1.X == point2.X)
        {
            //Waagerecht
            _lines.Add(Direction.Right, new Line(point1.X, point1.Y, point2.Y));
        }
        else
        {
            //Senkrecht
            _lines.Add(Direction.Down, new Line(point1.Y, point1.X, point2.X));
        }
    }

    private readonly struct Line
    {
        public readonly int Position { get; private init; }
        public readonly int Begin { get; private init; }
        public readonly int End { get; private init; }

        public Line(int position, int begin, int end)
        {
            Position = position;
            Begin = int.Min(begin, end);
            End = int.Max(begin, end);
        }
    }
}
