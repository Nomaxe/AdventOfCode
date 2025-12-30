using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;
    private readonly GridInt _grid;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2015, 6);
        _grid = new(1000);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (line.StartsWith("turn on"))
            {
                for (int y = numbers[1]; y <= numbers[3]; y++)
                {
                    for (int x = numbers[0]; x <= numbers[2]; x++)
                    {
                        _grid.SetValue(x, y, _grid.GetValue(x, y) + 1);
                    }
                }
            }
            else if (line.StartsWith("toggle"))
            {
                for (int y = numbers[1]; y <= numbers[3]; y++)
                {
                    for (int x = numbers[0]; x <= numbers[2]; x++)
                    {
                        _grid.SetValue(x, y, _grid.GetValue(x, y) + 2);
                    }
                }
            }
            else if (line.StartsWith("turn off"))
            {
                for (int y = numbers[1]; y <= numbers[3]; y++)
                {
                    for (int x = numbers[0]; x <= numbers[2]; x++)
                    {
                        _grid.SetValue(x, y, int.Max(_grid.GetValue(x, y) - 1, 0));
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return _grid.Sum().ToString();
    }
}
