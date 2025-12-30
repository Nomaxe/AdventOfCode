using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2022, 4);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (numbers[0] <= numbers[2] && numbers[1] >= numbers[3] || numbers[0] >= numbers[2] && numbers[1] <= numbers[3])
            {
                result++;
            }
        }

        return result.ToString();
    }
}
