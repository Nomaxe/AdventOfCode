using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe16b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<TicketRange> _ticketRange;
    private readonly DictionaryList<int, int> _ticketInformation;
    private readonly DictionaryHashSet<int, int> _possibleRanges;

    public Aufgabe16b()
    {
        _input = Utilities.ReadInput(2020, 16);
        _ticketRange = new();
        _ticketInformation = new();
        _possibleRanges = new();
    }

    public string Calc()
    {
        bool ticketRange = true;

        for (int i = 0; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                ticketRange = false;
                AddTicketInformation(_input[i + 2], false);
                i += 4;
                continue;
            }

            if (ticketRange)
            {
                _ticketRange.Add(new(_input[i]));
                continue;
            }

            AddTicketInformation(_input[i]);
        }

        for (int i = 0; i < _ticketRange.Count; i++)
        {
            var possibleRanges = GetPossibleTicketRanges(i);
            _possibleRanges.AddRange(i, possibleRanges);
        }

        _possibleRanges.RemoveDuplicatesUntilSingleItem();

        long result = 1;
        for (int i = 0; i <= 5; i++)
        {
            var keys = _possibleRanges.GetKeysOfItem(i);
            result *= _ticketInformation[keys.First()][0];
        }

        return result.ToString();

        //>932
    }

    private void AddTicketInformation(string input, bool checkPossible = true)
    {
        var numbers = input.GetUnsignedNumbers();

        if (checkPossible)
        {
            foreach (var number in numbers)
            {
                if (!IsInAnyRange(number))
                {
                    //Optimierung: Nur zu kleine und zu große Werte fliegen raus <30 || >974
                    return;
                }
            }
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            _ticketInformation.Add(i, numbers[i]);
        }
    }

    private bool IsInAnyRange(int number)
    {
        foreach (var range in _ticketRange)
        {
            if (range.IsPossible(number))
            {
                return true;
            }
        }

        return false;
    }

    private List<int> GetPossibleTicketRanges(int index)
    {
        List<int> possibleRanges = new();

        for (int i = 0; i < _ticketRange.Count; i++)
        {
            if (_ticketInformation[index].TrueForAll(_ticketRange[i].IsPossible))
            {
                possibleRanges.Add(i);
            }
        }

        return possibleRanges;
    }

    private readonly struct TicketRange
    {
        public int From1 { get; private init; }
        public int To1 { get; private init; }
        public int From2 { get; private init; }
        public int To2 { get; private init; }

        public TicketRange(string input)
        {
            var numbers = input.GetUnsignedNumbers();
            From1 = numbers[0];
            To1 = numbers[1];
            From2 = numbers[2];
            To2 = numbers[3];
        }

        public bool IsPossible(int number)
        {
            return number >= From1 && number <= To1 || number >= From2 && number <= To2;
        }

        public override string ToString()
        {
            return $"{From1}-{To1} or {From2}-{To2}";
        }
    }
}
