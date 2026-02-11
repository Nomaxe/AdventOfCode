using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe13b : IAufgabe
{
    private readonly string[] _input;
    private Grid _grid;
    private readonly List<(char Direction, int Number)> _folds = [];

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
    public Aufgabe13b()
    {
        _input = Utilities.ReadInput(2021, 13);
    }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

    public string Calc()
    {
        List<Point> points = [];

        bool whiteline = false;
        foreach (var line in _input)
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
