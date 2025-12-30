using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe05 : IAufgabe
{
    private readonly List<Range> _range;
    private readonly List<ulong> _numbers;

    public Aufgabe05()
    {
        var isRange = true;
        var input = Utilities.ReadInput(2025, 5);
        _range = [];
        _numbers = [];

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRange = false;
                continue;
            }

            if (isRange)
            {
                _range.Add(new(line));
            }
            else
            {
                _numbers.Add(ulong.Parse(line));
            }
        }

    }

    public string Calc()
    {
        var result = 0;

        foreach (var number in _numbers)
        {
            if (IsInRange(number))
            {
                result++;
            }
        }

        return result.ToString();
    }

    private bool IsInRange(ulong number)
    {
        foreach (var range in _range)
        {
            if (range.IsInRange(number))
            {
                return true;
            }
        }

        return false;
    }

    private readonly struct Range
    {
        private readonly ulong _from;
        private readonly ulong _to;

        public Range(string line)
        {
            var split = line.Split('-');
            _from = ulong.Parse(split[0]);
            _to = ulong.Parse(split[1]);
        }

        public bool IsInRange(ulong number)
        {
            return _from <= number && _to >= number;
        }
    }
}
