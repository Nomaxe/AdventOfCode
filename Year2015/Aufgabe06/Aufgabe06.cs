using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;
    private readonly GridBool _grid;

    public Aufgabe06()
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
                        _grid.SetValue(x, y, true);
                    }
                }
            }
            else if (line.StartsWith("toggle"))
            {
                for (int y = numbers[1]; y <= numbers[3]; y++)
                {
                    for (int x = numbers[0]; x <= numbers[2]; x++)
                    {
                        _grid.SetValue(x, y, !_grid.GetValue(x, y));
                    }
                }
            }
            else if (line.StartsWith("turn off"))
            {
                for (int y = numbers[1]; y <= numbers[3]; y++)
                {
                    for (int x = numbers[0]; x <= numbers[2]; x++)
                    {
                        _grid.SetValue(x, y, false);
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return _grid.GetCountOfValue(true).ToString();
    }
}
