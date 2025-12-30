using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe01b : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInputAsIntArray(2021, 1);
    }

    public string Calc()
    {
        int result = 0;

        for (int i = 3; i < _input.Length; i++)
        {
            if (_input[i] > _input[i - 3])
            {
                result++;
            }
        }

        return result.ToString();
    }
}
