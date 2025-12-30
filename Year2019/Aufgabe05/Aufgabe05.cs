using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe05 : IAufgabe
{
    public string Calc()
    {
        var intcode = new IntCode(2019, 5);
        intcode.AddInput(1);
        intcode.Calc();
        return intcode.Out.First(x => x != 0).ToString();
    }
}
