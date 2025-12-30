using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe20b : IAufgabe
{
    private readonly Blocklist[] _blocklist;

    public Aufgabe20b()
    {
        var input = Utilities.ReadInput(2016, 20);
        _blocklist = new Blocklist[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _blocklist[i] = new(input[i]);
        }
    }

    public string Calc()
    {
        Array.Sort(_blocklist);
        ulong currentSmallest = 0;
        ulong count = 0;

        for (int i = 0; i < _blocklist.Length; i++)
        {
            if (currentSmallest < _blocklist[i].From)
            {
                count += _blocklist[i].From - currentSmallest;
            }

            if (_blocklist[i].To > currentSmallest)
            {
                currentSmallest = _blocklist[i].To + 1;
            }
        }

        return count.ToString();
    }

    private readonly struct Blocklist : IComparable<Blocklist>
    {
        public ulong From { get; private init; }
        public ulong To { get; private init; }

        public Blocklist(string input)
        {
            var numbers = input.Split('-');
            From = uint.Parse(numbers[0]);
            To = uint.Parse(numbers[1]);
        }

        public int CompareTo(Blocklist other)
        {
            return From.CompareTo(other.From);
        }
    }
}
