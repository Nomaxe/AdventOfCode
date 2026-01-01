using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe01 : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInputAsArray<int>(2021, 1);
    }

    public string Calc()
    {
        int result = 0;

        for (int i = 1; i < _input.Length; i++)
        {
            if (_input[i] > _input[i - 1])
            {
                result++;
            }
        }

        return result.ToString();
    }
}
