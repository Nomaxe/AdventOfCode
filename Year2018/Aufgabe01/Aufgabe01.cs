using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe01 : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInputAsArray<int>(2018, 1);
    }

    public string Calc()
    {
        return _input.Sum().ToString();
    }
}
