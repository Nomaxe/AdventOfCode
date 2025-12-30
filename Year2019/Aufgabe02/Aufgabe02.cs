using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe02 : IAufgabe
{
    public string Calc()
    {
        var intcode = new IntCode(2019, 2);
        intcode.SetCode(1, 12);
        intcode.SetCode(2, 2);
        intcode.Calc();
        return intcode.Codes[0].ToString();
    }
}
