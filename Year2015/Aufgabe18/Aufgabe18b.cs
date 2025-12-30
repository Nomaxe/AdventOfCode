using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe18b : IAufgabe
{
    private Grid _grid;

    public Aufgabe18b()
    {
        _grid = Grid.CreateCharGrid(2015, 18);
    }

    public string Calc()
    {
        for (int i = 0; i < 100; i++)
        {
            Grid nextGrid = new(_grid.SizeX, _grid.SizeY);

            for (int y = 0; y < _grid.SizeY; y++)
            {
                for (int x = 0; x < _grid.SizeX; x++)
                {
                    if ((x == 0 || x == _grid.SizeX - 1) && (y == 0 || y == _grid.SizeY - 1))
                    {
                        nextGrid.SetValue(x, y, '#');
                        continue;
                    }

                    var currentState = _grid.GetValue(x, y);
                    var count = GetCount(new(x, y));

                    if (currentState == '#')
                    {
                        if (count == 2 || count == 3)
                        {
                            nextGrid.SetValue(x, y, '#');
                        }
                        else
                        {
                            nextGrid.SetValue(x, y, ' ');
                        }
                    }
                    else
                    {
                        if (count == 3)
                        {
                            nextGrid.SetValue(x, y, '#');
                        }
                        else
                        {
                            nextGrid.SetValue(x, y, ' ');
                        }
                    }
                }
            }

            _grid = nextGrid;
        }

        return _grid.GetCountOfValue('#').ToString();
    }

    private int GetCount(Point point)
    {
        return _grid.GetInBoundFullNeighbours(point).Select(x => _grid.GetValue(x)).Count(x => x == '#');
    }
}
