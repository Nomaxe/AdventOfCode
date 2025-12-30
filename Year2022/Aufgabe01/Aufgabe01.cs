using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe01 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2022, 1);
    }

    public string Calc()
    {
        int max = 0;
        int current = 0;

        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                max = int.Max(max, current);
                current = 0;
            }
            else
            {
                current += int.Parse(line);
            }
        }

        return max.ToString();
    }
}
