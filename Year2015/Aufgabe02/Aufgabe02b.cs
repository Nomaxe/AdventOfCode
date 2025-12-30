using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2015, 2);
    }

    public string Calc()
    {
        ulong result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            if (numbers[0] <= numbers[1])
            {
                if (numbers[1] <= numbers[2])
                {
                    result += (ulong)(2 * numbers[0] + 2 * numbers[1]);
                }
                else
                {
                    result += (ulong)(2 * numbers[0] + 2 * numbers[2]);
                }
            }
            else
            {
                if (numbers[0] <= numbers[2])
                {
                    result += (ulong)(2 * numbers[0] + 2 * numbers[1]);
                }
                else
                {
                    result += (ulong)(2 * numbers[1] + 2 * numbers[2]);
                }
            }

            result += (ulong)(numbers[0] * numbers[1] * numbers[2]);
        }

        return result.ToString();
    }
}
