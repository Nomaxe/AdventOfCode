using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;
    private readonly GridInt _grid;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2021, 5);
        _grid = new(1000);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (numbers[0] != numbers[2] && numbers[1] != numbers[3])
            {
                continue;
            }

            if (numbers[0] > numbers[2])
            {
                (numbers[0], numbers[2]) = (numbers[2], numbers[0]);
            }
            if (numbers[1] > numbers[3])
            {
                (numbers[1], numbers[3]) = (numbers[3], numbers[1]);
            }

            for (int y = numbers[1]; y <= numbers[3]; y++)
            {
                for (int x = numbers[0]; x <= numbers[2]; x++)
                {
                    _grid.SetValue(x, y, _grid.GetValue(x, y) + 1);
                }
            }
        }

        return _grid.GetCountOf(x => x > 1).ToString();
    }
}
