using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe01b : IAufgabe
{
    public readonly int[] _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInputAsIntArray(2019, 1);
    }

    public string Calc()
    {
        long result = 0;

        foreach (var i in _input)
        {
            result += GetFuel(i);
        }

        return result.ToString();
    }

    private static long GetFuel(long value)
    {
        if (value < 9)
        {
            return 0;
        }

        long fuel = value / 3 - 2;

        return fuel + GetFuel(fuel);
    }
}
