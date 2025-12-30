using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2015, 2);
    }

    public string Calc()
    {
        ulong result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            var lw = numbers[0] * numbers[1];
            var wh = numbers[1] * numbers[2];
            var lh = numbers[0] * numbers[2];

            result += (ulong)(2 * lw + 2 * wh + 2 * lh + int.Min(lw, int.Min(wh, lh)));
        }

        return result.ToString();
    }
}
