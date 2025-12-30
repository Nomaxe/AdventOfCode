using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe04b()
    {
        _input = Utilities.ReadInput(2022, 4);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (numbers[0] <= numbers[3] && numbers[1] >= numbers[2])
            {
                result++;
            }
            else
            {

            }
        }

        return result.ToString();
    }
}
