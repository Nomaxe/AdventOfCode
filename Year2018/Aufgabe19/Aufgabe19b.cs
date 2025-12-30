using AdventOfCode.Utils;
using AdventOfCode.Year2018.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe19b : IAufgabe
{
    private readonly Device _device;

    public Aufgabe19b()
    {
        var input = Utilities.ReadInput(2018, 19);
        _device = new();
        foreach (var line in input)
        {
            _device.AddInstruction(line);
        }
    }

    public string Calc()
    {
        _device.RegisterA = 1;
        _device.MaxSteps = 20;
        _device.Calc();
        int input = _device.RegisterE;

        int result = 0;

        for (int i = 1; i <= input / 2; i++)
        {
            if (input % i == 0)
            {
                result += i;
            }
        }

        result += input;
        return result.ToString();
    }
}
