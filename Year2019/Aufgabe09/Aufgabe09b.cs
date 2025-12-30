using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe09b : IAufgabe
{
    public string Calc()
    {
        var intcode = new IntCode(2019, 9);
        intcode.AddInput(2);
        intcode.Calc();
        return string.Join(',', intcode.Out);
    }
}
