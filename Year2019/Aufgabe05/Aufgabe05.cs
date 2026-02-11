using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe05 : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe05()
    {
        _intCode = new IntCode(2019, 5);
    }

    public string Calc()
    {
        _intCode.AddInput(1);
        _intCode.Calc();
        return _intCode.Out.First(x => x != 0).ToString();
    }
}
