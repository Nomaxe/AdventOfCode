using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe09 : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe09()
    {
        _intCode = new IntCode(2019, 9);
    }

    public string Calc()
    {
        _intCode.AddInput(1);
        _intCode.Calc();
        return string.Join(',', _intCode.Out);
    }
}
