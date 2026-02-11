using AdventOfCode.Utils;
using AdventOfCode.Year2016.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe23b : IAufgabe
{
    private readonly AssembunnyCode _code;

    public Aufgabe23b()
    {
        _code = new(2016, 23);
    }

    public string Calc()
    {
        _code.SetRegister('a', 12);
        _code.Calc();
        return _code.GetRegister('a').ToString();
    }
}
