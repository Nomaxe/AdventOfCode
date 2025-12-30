using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2021;

internal class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, char> _rules;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2021, 14);
        _rules = new(_input.Length - 2);
    }

    public string Calc()
    {
        var currentString = _input[0];

        foreach (var line in _input.Skip(2))
        {
            _rules.Add(line[..2], line[^1]);
        }

        for (int i = 0; i < 10; i++)
        {
            StringBuilder builder = new(currentString.Length * 2 - 1);
            for (int j = 0; j < currentString.Length - 1; j++)
            {
                builder.Append(currentString[j]);
                builder.Append(_rules[currentString[j..(j + 2)]]);
            }
            builder.Append(currentString[^1]);

            currentString = builder.ToString();
        }

        DictionaryCounter<char> counter = new(currentString.Select(x => x));
        (var max, var min) = counter.GetMaxMin();

        return (max - min).ToString();
    }
}
