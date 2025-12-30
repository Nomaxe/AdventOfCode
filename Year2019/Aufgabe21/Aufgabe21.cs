using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe21 : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe21()
    {
        _intCode = new(2019, 21);
    }

    public string Calc()
    {
        _intCode.AddInput("NOT A J");
        _intCode.AddInput("NOT B T");
        _intCode.AddInput("OR T J");
        _intCode.AddInput("NOT C T");
        _intCode.AddInput("OR T J");
        _intCode.AddInput("AND D J");
        _intCode.AddInput("WALK");
        _intCode.Calc();

        return _intCode.Out[^1].ToString();
    }
}
