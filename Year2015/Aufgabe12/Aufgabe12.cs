using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe12 : IAufgabe
{
    private readonly string _input;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2015, 12)[0];
    }

    public string Calc()
    {
        return _input.GetNumbers().Sum().ToString();
    }
}
