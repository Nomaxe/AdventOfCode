using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe15 : IAufgabe
{
    private readonly Disc[] _discs;

    public Aufgabe15()
    {
        var input = Utilities.ReadInput(2016, 15);
        _discs = new Disc[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _discs[i] = new(input[i]);
        }
    }

    public string Calc()
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            if (Check(i))
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }

    private bool Check(int starttime)
    {
        for (int i = 0; i < _discs.Length; i++)
        {
            if (!_discs[i].Check(starttime + i + 1))
            {
                return false;
            }
        }

        return true;
    }

    private readonly struct Disc
    {
        public int PositionCount { get; private init; }
        public int StartPosition { get; private init; }

        public Disc(string input)
        {
            var numbers = input.GetNumbers();
            PositionCount = numbers[1];
            StartPosition = numbers[3];
        }

        public bool Check(int time)
        {
            return (StartPosition + time) % PositionCount == 0;
        }
    }
}
