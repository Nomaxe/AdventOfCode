using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe13 : IAufgabe
{
    private Grid _grid;
    private readonly List<(char Direction, int Number)> _folds = [];

    public Aufgabe13()
    {
        List<Point> points = [];

        var input = Utilities.ReadInput(2021, 13);
        bool whiteline = false;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                whiteline = true;
                continue;
            }

            if (!whiteline)
            {
                points.Add(new(line));
            }
            else
            {
                _folds.Add((line[11], int.Parse(line[13..])));
            }
        }

        _grid = new(_folds.First(x => x.Direction == 'x').Number * 2 + 1, _folds.First(x => x.Direction == 'y').Number * 2 + 1, '.');
        foreach (var point in points)
        {
            _grid.SetValue(point, '#');
        }
    }

    public string Calc()
    {
        Grid nextGrid;

        if (_folds[0].Direction == 'x')
        {
            nextGrid = new(_folds[0].Number, _grid.SizeY, '.');
        }
        else
        {
            nextGrid = new(_grid.SizeX, _folds[0].Number, '.');
        }

        for (int y = 0; y < nextGrid.SizeY; y++)
        {
            for (int x = 0; x < nextGrid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) == '#')
                {
                    nextGrid.SetValue(x, y, '#');
                }

                int otherX, otherY;

                if (_folds[0].Direction == 'x')
                {
                    otherX = _grid.SizeX - x - 1;
                    otherY = y;
                }
                else
                {
                    otherX = x;
                    otherY = _grid.SizeY - y - 1;
                }

                if (_grid.GetValue(otherX, otherY) == '#')
                {
                    nextGrid.SetValue(x, y, '#');
                }
            }
        }

        _grid = nextGrid;

        return _grid.GetCountOfValue('#').ToString();
    }
}
