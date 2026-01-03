using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe10b : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInputAsArray<int>(2020, 10);
    }

    public string Calc()
    {
        _input.Sort();

        int lastValue = 0;
        int count = 1;
        ulong result = 1;

        foreach (var number in _input)
        {
            var difference = number - lastValue;
            if (difference == 3)
            {
                result *= GetCount(count);
                count = 0;
            }

            lastValue = number;
            count++;
        }

        result *= GetCount(count);

        return result.ToString();
    }

    private static ulong GetCount(int count)
    {
        return count switch
        {
            1 => 1,
            2 => 1,
            3 => 2,
            4 => 4,
            5 => 7,
            _ => throw new NotImplementedException()
        };
    }
}
