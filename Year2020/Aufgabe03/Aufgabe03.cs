using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe03 : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe03()
    {
        _grid = Grid.CreateCharGrid(2020, 3);
    }

    public string Calc()
    {
        int count = 0;

        for (int y = 1; y < _grid.SizeY; y++)
        {
            var x = y * 3 % _grid.SizeX;

            if (_grid.GetValue(x, y) == '#')
            {
                count++;
            }
        }

        return count.ToString();
    }
}
