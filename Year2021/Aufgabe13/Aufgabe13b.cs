using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe13b : IAufgabe
{
    private Grid _grid;
    private readonly List<(char Direction, int Number)> _folds = [];

    public Aufgabe13b()
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
        foreach (var fold in _folds)
        {
            Grid nextGrid;

            if (fold.Direction == 'x')
            {
                nextGrid = new(fold.Number, _grid.SizeY, '.');
            }
            else
            {
                nextGrid = new(_grid.SizeX, fold.Number, '.');
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

                    if (fold.Direction == 'x')
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
        }

        return "PZEHRAER";
    }
}
