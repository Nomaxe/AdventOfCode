using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2016;

internal class Aufgabe22b : IAufgabe
{
    private readonly Grid _grid;
    private readonly Point _emptyPoint;

    public Aufgabe22b()
    {
        var input = Utilities.ReadInput(2016, 22);
        var sizeX = input[^1].GetNumber(16) + 1;
        _grid = new(sizeX, (input.Length - 2) / sizeX);

        foreach (var line in input.Skip(2))
        {
            var x = line.GetNumber(16);
            var y = line.GetNumber(x >= 10 ? 20 : 19);
            var size = line.GetNumberWhitespace(24);
            var used = line.GetNumberWhitespace(30);

            _grid.SetValue(x, y, size >= 100 ? '#' : ' ');

            if (used == 0)
            {
                _emptyPoint = new(x, y);
            }
        }
    }

    public string Calc()
    {
        CompleteSolver solver = new(_grid);
        solver.SolveLabyrinth(_emptyPoint);

        return (solver.GetLength(new(_grid.SizeX - 1, 0)) + 5 * (_grid.SizeX - 2)).ToString();
    }
}
