using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe01b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInput(2022, 1);
    }

    public string Calc()
    {
        List<int> max = [];
        int current = 0;

        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                max.Add(current);
                current = 0;
            }
            else
            {
                current += int.Parse(line);
            }
        }

        return max.OrderDescending().Take(3).Sum().ToString();
    }
}
