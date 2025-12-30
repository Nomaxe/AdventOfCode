using AdventOfCode.Utils;
using AdventOfCode.Year2017.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe10b : IAufgabe
{
    private readonly string _input;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2017, 10)[0];
    }

    public string Calc()
    {
        KnotHash hash = new(_input);
        hash.Calc();

        return hash.GetResult().ToString();
    }
}
