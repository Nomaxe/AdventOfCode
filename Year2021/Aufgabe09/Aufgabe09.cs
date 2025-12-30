using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe09 : IAufgabe
{
    private readonly GridInt _grid;

    public Aufgabe09()
    {
        _grid = GridInt.CreateIntGrid(2021, 9);
    }

    public string Calc()
    {
        int result = 0;

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                int height = _grid.GetValue(x, y);

                if (_grid.GetInBoundNeighbours(x, y).All(x => _grid.GetValue(x) > height))
                {
                    result += height + 1;
                }
            }
        }

        return result.ToString();
    }
}
