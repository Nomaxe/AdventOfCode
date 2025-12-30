using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe05b : IAufgabe
{
    public string Calc()
    {
        var intcode = new IntCode(2019, 5);
        intcode.AddInput(5);
        intcode.Calc();
        return string.Join(',', intcode.Out);
    }
}
