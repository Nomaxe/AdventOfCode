using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2016;

internal class Aufgabe13 : IAufgabe
{
    private readonly Grid _grid;

    public Aufgabe13()
    {
        const int GridSize = 50;

        var input = Utilities.ReadInputAsInt(2016, 13);
        _grid = new(GridSize);
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                var number = x * x + 3 * x + 2 * x * y + y + y * y;
                number += input;
                _grid.SetValue(x, y, number.ToString("B").Count(x => x == '1') % 2 == 0 ? ' ' : '#');

            }
        }
    }

    public string Calc()
    {
        CompleteSolver solver = new(_grid);
        solver.SolveLabyrinth(new(1, 1));
        return solver.GetLength(new(31, 39)).ToString();
    }
}