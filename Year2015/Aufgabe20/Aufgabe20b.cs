using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe20b : IAufgabe
{
    public readonly int _input;

    public Aufgabe20b()
    {
        _input = Utilities.ReadInputAsT<int>(2015, 20);
    }

    public string Calc()
    {
        int[] numbers = new int[_input / 10];

        for (int i = 1; i < numbers.Length; i++)
        {
            int number = i;

            for (int j = 0; j < 50 && number < numbers.Length; j++)
            {
                numbers[number] += i * 11;
                number += i;
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
