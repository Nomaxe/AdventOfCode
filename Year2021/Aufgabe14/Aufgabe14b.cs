using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe14b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, char> _rules;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInput(2021, 14);
        _rules = new(_input.Length - 2);
    }

    public string Calc()
    {
        LargeCounter<string> counter = new(_input[0].Length - 1);

        for (int i = 0; i < _input[0].Length - 1; i++)
        {
            counter.Add(_input[0][i..(i + 2)]);
        }

        foreach (var line in _input.Skip(2))
        {
            _rules.Add(line[..2], line[^1]);
        }

        for (int i = 0; i < 40; i++)
        {
            LargeCounter<string> nextCounter = new(counter.Count);

            foreach (var pair in counter)
            {
                var character = _rules[pair.Key];
                nextCounter.Add($"{pair.Key[0]}{character}", pair.Value);
                nextCounter.Add($"{character}{pair.Key[1]}", pair.Value);
            }

            counter = nextCounter;
        }

        LargeCounter<char> resultCounter = [];
        foreach (var pair in counter)
        {
            resultCounter.Add(pair.Key[0], pair.Value);
        }
        resultCounter.Add(_input[0][^1]);

        (var max, var min) = resultCounter.GetMaxMin();

        return (max - min).ToString();
    }
}
