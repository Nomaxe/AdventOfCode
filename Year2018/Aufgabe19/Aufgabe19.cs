using AdventOfCode.Utils;
using AdventOfCode.Year2018.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe19 : IAufgabe
{
    private readonly Device _device = new();

    public Aufgabe19()
    {
        var input = Utilities.ReadInput(2018, 19);
        foreach (var line in input)
        {
            _device.AddInstruction(line);
        }
    }

    public string Calc()
    {
        _device.Calc();
        return _device.RegisterA.ToString();
    }
}
