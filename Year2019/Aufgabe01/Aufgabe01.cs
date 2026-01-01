using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe01 : IAufgabe
{
    public readonly int[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInputAsArray<int>(2019, 1);
    }

    public string Calc()
    {
        long result = 0;

        foreach (var i in _input)
        {
            result += i / 3 - 2;
        }

        return result.ToString();
    }
}
