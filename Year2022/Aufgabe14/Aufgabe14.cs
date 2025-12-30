using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private readonly Grid _grid;
    private readonly Point _startPosition;
    private int _currentX;
    private int _currentY;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2022, 14);
        _grid = new(1000, 250, '.');
        _startPosition = new(500, 0);

        _currentX = _startPosition.X;
        _currentY = _startPosition.Y;
    }

    public string Calc()
    {
        int count = 0;

        foreach (var line in _input)
        {
            var split = line.Split(" -> ").Select(x => new Point(x)).ToList();

            for (int i = 1; i < split.Count; i++)
            {
                if (split[i].X == split[i - 1].X)
                {
                    FillHorizontal(split[i - 1], split[i]);
                }
                else
                {
                    FillVertical(split[i - 1], split[i]);
                }
            }
        }

        while (_currentY < _grid.SizeY - 1)
        {
            var check = _grid.GetValue(_currentX, _currentY + 1);
            if (check == '.')
            {
                _currentY++;
                continue;
            }

            check = _grid.GetValue(_currentX - 1, _currentY + 1);
            if (check == '.')
            {
                _currentX--;
                _currentY++;
                continue;
            }

            check = _grid.GetValue(_currentX + 1, _currentY + 1);
            if (check == '.')
            {
                _currentX++;
                _currentY++;
                continue;
            }

            _grid.SetValue(_currentX, _currentY, 'O');
            count++;
            _currentX = _startPosition.X;
            _currentY = _startPosition.Y;
        }

        return count.ToString();
    }

    private void FillHorizontal(Point from, Point to)
    {
        if (from.Y > to.Y)
        {
            (from, to) = (to, from);
        }

        for (int y = from.Y; y <= to.Y; y++)
        {
            _grid.SetValue(from.X, y, '#');
        }
    }

    private void FillVertical(Point from, Point to)
    {
        if (from.X > to.X)
        {
            (from, to) = (to, from);
        }

        for (int x = from.X; x <= to.X; x++)
        {
            _grid.SetValue(x, from.Y, '#');
        }
    }
}
