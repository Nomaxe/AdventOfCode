using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe05b : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe05b()
    {
        _intCode = new IntCode(2019, 5);
    }

    public string Calc()
    {
        _intCode.AddInput(5);
        _intCode.Calc();
        return string.Join(',', _intCode.Out);
    }
}
