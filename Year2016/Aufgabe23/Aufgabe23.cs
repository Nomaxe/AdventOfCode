using AdventOfCode.Utils;
using AdventOfCode.Year2016.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe23 : IAufgabe
{
    private readonly AssembunnyCode _code;

    public Aufgabe23()
    {
        _code = new(2016, 23);
    }

    public string Calc()
    {
        _code.SetRegister('a', 7);
        _code.Calc();
        return _code.GetRegister('a').ToString();
    }
}
