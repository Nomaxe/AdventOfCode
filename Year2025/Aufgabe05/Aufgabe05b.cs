using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe05b : IAufgabe
{
    private readonly string[] _input;
    private List<Range> _range;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2025, 5);
        _range = [];
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            _range.Add(new(line));
        }

        _range = _range.OrderBy(x => x.From).ToList();

        for (int i = 0; i < _range.Count - 1; i++)
        {
            if (_range[i].To >= _range[i + 1].From)
            {
                _range[i] = new(_range[i].From, ulong.Max(_range[i].To, _range[i + 1].To));
                _range.RemoveAt(i + 1);
                i--;
            }
        }

        ulong result = 0;

        foreach (var range in _range)
        {
            result += range.To - range.From + 1;
        }

        return result.ToString();
    }

    private readonly struct Range
    {
        public ulong From { get; private init; }
        public ulong To { get; private init; }

        public Range(string line)
        {
            var split = line.Split('-');
            From = ulong.Parse(split[0]);
            To = ulong.Parse(split[1]);
        }

        public Range(ulong from, ulong to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From:N0}-{To:N0}";
        }
    }
}
