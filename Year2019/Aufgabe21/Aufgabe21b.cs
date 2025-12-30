using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe21b : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe21b()
    {
        _intCode = new(2019, 21);
    }

    public string Calc()
    {
        _intCode.AddInput("NOT H J");
        _intCode.AddInput("OR C J");
        _intCode.AddInput("AND B J");
        _intCode.AddInput("AND A J");
        _intCode.AddInput("NOT J J");
        _intCode.AddInput("AND D J");
        _intCode.AddInput("RUN");
        _intCode.Calc();

        return _intCode.Out[^1].ToString();
    }
}
