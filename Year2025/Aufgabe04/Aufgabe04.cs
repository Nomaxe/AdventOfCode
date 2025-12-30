using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe04 : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe04()
    {
        _grid = Grid.CreateCharGrid(2025, 4);
    }

    public string Calc()
    {
        var result = 0;

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) != '@')
                {
                    continue;
                }

                var count = 0;

                foreach (var neighbour in _grid.GetInBoundFullNeighbours(x, y))
                {
                    if (_grid.GetValue(neighbour) == '@')
                    {
                        count++;
                    }
                }

                if (count < 4)
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }
}
