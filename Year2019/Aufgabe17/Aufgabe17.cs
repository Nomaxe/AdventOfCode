using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe17 : IAufgabe
{
    private readonly IntCode _intcode;
    private readonly Grid _grid;

    public Aufgabe17()
    {
        _intcode = new(2019, 17);
        _grid = new(50, 50);
    }

    public string Calc()
    {
        int x = 0;
        int y = 0;


        _intcode.Calc();

        foreach (var character in _intcode.Out)
        {
            if (character == IntCode.NewLineNumber)
            {
                x = 0;
                y++;
                continue;
            }

            _grid.SetValue(x, y, (char)character);
            x++;
        }

        int result = 0;
        foreach (var point in _grid.GetPointsOfValue('#'))
        {
            if (IsOverlap(point))
            {
                result += point.X * point.Y;
            }
        }

        return result.ToString();
    }

    private bool IsOverlap(Point point)
    {
        if (point.X == 0 || point.Y == 0)
        {
            return false;
        }

        return _grid.GetValue(point.X - 1, point.Y) == '#' &&
               _grid.GetValue(point.X, point.Y + 1) == '#' &&
               _grid.GetValue(point.X + 1, point.Y) == '#' &&
               _grid.GetValue(point.X, point.Y - 1) == '#';
    }
}
