using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe13 : IAufgabe
{
    private readonly IntCode _intcode;

    public Aufgabe13()
    {
        _intcode = new(2019, 13);
    }

    public string Calc()
    {
        _intcode.Calc();

        return _intcode.Out.Where((x, i) => (i + 1) % 3 == 0).Count(x => x == 2).ToString();
    }
}
