using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe02 : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe02()
    {
        _intCode = new(2019, 2);
    }

    public string Calc()
    {
        _intCode.SetCode(1, 12);
        _intCode.SetCode(2, 2);
        _intCode.Calc();
        return _intCode.Codes[0].ToString();
    }
}
