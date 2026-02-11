using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe11b : IAufgabe
{
    private readonly List<ulong> _input;
    private LargeCounter<ulong> _counter;

    public Aufgabe11b()
    {
        _input = Utilities.ReadInputAsList<ulong>(2024, 11, ' ');
        _counter = new(_input.Count);
    }

    public string Calc()
    {
        const int amount = 75;

        foreach (var number in _input)
        {
            _counter.Add(number);
        }

        for (int i = 1; i <= amount; i++)
        {
            LargeCounter<ulong> nextCounter = [];

            foreach (var number in _counter)
            {
                if (number.Key == 0)
                {
                    nextCounter.Add(1, number.Value);
                    continue;
                }

                var text = number.Key.ToString();
                if (text.Length % 2 == 0)
                {
                    nextCounter.Add(ulong.Parse(text[..(text.Length / 2)]), number.Value);
                    nextCounter.Add(ulong.Parse(text[(text.Length / 2)..]), number.Value);
                }
                else
                {
                    nextCounter.Add(number.Key * 2024, number.Value);
                }
            }

            _counter = nextCounter;
        }

        return _counter.GetTotalCount().ToString();
    }
}
