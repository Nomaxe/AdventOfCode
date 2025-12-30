using AdventOfCode.Utils;
using AdventOfCode.Year2017.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe10 : IAufgabe
{
    private readonly List<int> _input;

    public Aufgabe10()
    {
        _input = Utilities.ReadInputAsIntList(2017, 10);
    }

    public string Calc()
    {
        KnotHash hash = new(_input);
        hash.Calc();

        return (hash.List[0] * hash.List[1]).ToString();
    }
}
