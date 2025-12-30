using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2016;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2016, 6);
    }

    public string Calc()
    {
        List<LargeCounter<char>> counters = [];

        foreach (var character in _input[0])
        {
            counters.Add([]);
        }

        foreach (var line in _input)
        {
            for (int i = 0; i < line.Length; i++)
            {
                counters[i].Add(line[i]);
            }
        }

        StringBuilder builder = new();
        foreach (var counter in counters)
        {
            builder.Append(counter.GetMaxKey());
        }

        return builder.ToString();
    }
}
