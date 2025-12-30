using AdventOfCode.Utils;
using AdventOfCode.Year2018.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe21 : IAufgabe
{
    private readonly Device _device;

    public Aufgabe21()
    {
        _device = new();
        var input = Utilities.ReadInput(2018, 21);
        _device.StopAtReadOfRegister0 = true;
        foreach (var line in input)
        {
            _device.AddInstruction(line);
        }
    }

    public string Calc()
    {
        _device.Calc();

        return _device.GetResultOfCurrentResultRegister().ToString();
    }
}