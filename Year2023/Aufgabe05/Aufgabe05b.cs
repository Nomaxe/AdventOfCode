using AdventOfCode.Utils;
using System.Diagnostics;

namespace AdventOfCode.Year2023;

internal class Aufgabe05b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<List<long[]>> _numbers;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2023, 5);
        _numbers = [];
    }

    public string Calc()
    {
        var seeds = _input[0].GetUnsignedLongNumbers();
        List<long[]> currentList = [];
        _numbers.Add(currentList);

        for (int i = 3; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                i++;
                currentList = [];
                _numbers.Add(currentList);
                continue;
            }

            currentList.Add(_input[i].GetUnsignedLongNumbers());
        }

        foreach (var numbers in _numbers)
        {
            numbers.Sort((x, y) => x[1].CompareTo(y[1]));
        }

        var min = long.MaxValue;

        for (int i = 0; i < seeds.Length; i += 2)
        {
            List<NumberRange> range = [NumberRange.CreateNumberRange(seeds[i], seeds[i + 1])];

            foreach (var numbers in _numbers)
            {
                List<NumberRange> nextRange = [];

                foreach (var rangeElement in range)
                {
                    GetNumbers(rangeElement, nextRange, numbers);
                }

                range = nextRange.OrderBy(x => x.Start).ToList();
            }

            min = long.Min(min, range.Min(x => x.Start));
        }

        return min.ToString();
    }

    private static void GetNumbers(NumberRange range, List<NumberRange> nextRange, List<long[]> numbersList)
    {
        foreach (var numbers in numbersList)
        {
            var numberStart = numbers[1];
            var numberEnd = numbers[1] + numbers[2] - 1;

            if (range.End < numberStart)
            {
                nextRange.Add(range);
                return;
            }

            if (range.Start > numberEnd)
            {
                continue;
            }

            if (range.Start <= numberStart)
            {
                if (range.Start < numberStart)
                {
                    nextRange.Add(new(range.Start, numberStart - 1));
                    range.Start = numberStart;
                }

                if (range.End >= numberEnd)
                {
                    nextRange.Add(NumberRange.CreateNumberRange(numbers[0], numbers[2]));
                    range.Start = numberEnd + 1;
                }
                else
                {
                    nextRange.Add(NumberRange.CreateNumberRange(numbers[0], range.Length));
                    return;
                }
            }
            else
            {
                var offset = range.Start - numbers[1];

                if (range.End > numberEnd)
                {
                    nextRange.Add(NumberRange.CreateNumberRange(numbers[0] + offset, numberEnd - range.Start + 1));
                    range.Start = numberEnd + 1;
                }
                else
                {
                    nextRange.Add(NumberRange.CreateNumberRange(numbers[0] + offset, range.Length));
                    return;
                }
            }
        }

        nextRange.Add(range);
    }

    private struct NumberRange
    {
        private long _start;
        private long _end;
        public long Start
        {
            readonly get
            {
                return _start;
            }
            set
            {
                _start = value;
                Debug.Assert(Start <= End);
            }
        }
        public long End
        {
            readonly get
            {
                return _end;
            }
            set
            {
                _end = value;
                Debug.Assert(Start <= End);
            }
        }
        public readonly long Length => End - Start + 1;

        public NumberRange(long start, long end)
        {
            _start = start;
            _end = end;

            Debug.Assert(start <= end);
        }

        public static NumberRange CreateNumberRange(long start, long length)
        {
            return new(start, start + length - 1);
        }

        public readonly override string ToString()
        {
            return $"{Start:N0} - {End:N0}";
        }
    }
}
