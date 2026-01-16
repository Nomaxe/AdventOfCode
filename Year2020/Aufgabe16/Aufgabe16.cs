using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe16 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Range> _range;

    public Aufgabe16()
    {
        _input = Utilities.ReadInput(2020, 16);
        _range = new();
    }

    public string Calc()
    {
        int result = 0;
        bool ranges = true;

        for (int i = 0; i < _input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(_input[i]))
            {
                i += 4;
                ranges = false;
                continue;
            }

            var numbers = _input[i].GetUnsignedNumbers();
            if (ranges)
            {
                _range.Add(new(numbers[0], numbers[1]));
                _range.Add(new(numbers[2], numbers[3]));
                continue;
            }

            foreach (var number in numbers)
            {
                if (!IsInRange(number))
                {
                    result += number;
                }
            }
        }

        return result.ToString();
    }

    private bool IsInRange(int number)
    {
        foreach (var range in _range)
        {
            if (number >= range.From && number <= range.To)
            {
                return true;
            }
        }

        return false;
    }

    private readonly struct Range
    {
        public int From { get; private init; }
        public int To { get; private init; }

        public Range(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
