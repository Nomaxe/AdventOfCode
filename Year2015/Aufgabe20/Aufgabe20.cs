using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe20 : IAufgabe
{
    public readonly int _input;

    public Aufgabe20()
    {
        _input = Utilities.ReadInputAsT<int>(2015, 20);
    }

    public string Calc()
    {
        int[] numbers = new int[_input / 10];

        for (int i = 1; i < numbers.Length; i++)
        {
            for (int j = i; j < numbers.Length; j += i)
            {
                numbers[j] += i * 10;
            }
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] >= _input)
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
