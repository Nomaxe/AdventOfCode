using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe13 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe13()
    {
        _input = Utilities.ReadInput(2017, 13);
    }

    public string Calc()
    {
        int severity = 0;
        foreach (var line in _input)
        {
            var split = line.Split(": ");
            var step = int.Parse(split[0]);
            var depth = int.Parse(split[1]);

            if (step % ((depth - 1) * 2) == 0)
            {
                severity += step * depth;
            }
        }

        return severity.ToString();
    }
}
