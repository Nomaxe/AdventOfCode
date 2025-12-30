using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe03b : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe03b()
    {
        _grid = Grid.CreateCharGrid(2020, 3);
    }

    public string Calc()
    {
        return (GetCount(1, 1) * GetCount(3, 1) * GetCount(5, 1) * GetCount(7, 1) * GetCount(1, 2)).ToString();
    }

    private long GetCount(int xOffset, int yOffset)
    {
        long count = 0;

        for (int y = 0; y < _grid.SizeY; y += yOffset)
        {
            var x = (y / yOffset) * xOffset % _grid.SizeX;

            if (_grid.GetValue(x, y) == '#')
            {
                count++;
            }
        }

        return count;
    }
}
