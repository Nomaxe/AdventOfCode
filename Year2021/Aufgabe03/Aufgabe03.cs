using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe03 : IAufgabe
{
    private readonly GridBool _grid;

    public Aufgabe03()
    {
        _grid = GridBool.CreateBoolGrid(2021, 3, '1');
    }

    public string Calc()
    {
        LargeCounter<bool>[] counter = new LargeCounter<bool>[_grid.SizeX];
        for (int i = 0; i < counter.Length; i++)
        {
            counter[i] = new(2);
        }

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                counter[x].Add(_grid.GetValue(x, y));
            }
        }

        var mostCommon = counter.Select(x => x.GetMaxKey()).ToList();

        return (mostCommon.GetDecimalNumber() * mostCommon.Select(x => !x).ToList().GetDecimalNumber()).ToString();
    }
}
