using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe16 : IAufgabe
{
    private int[] _value;

    private readonly int[] _pattern = [0, 1, 0, -1];

    public Aufgabe16()
    {
        _value = Utilities.ReadInput(2019, 16)[0].Select(x => x.ToNumber()).ToArray();
    }

    public string Calc()
    {
        for (int i = 0; i < 100; i++)
        {
            int[] nextValue = new int[_value.Length];

            for (int j = 0; j < _value.Length; j++)
            {
                nextValue[j] = GetNextValue(j);
            }

            _value = nextValue;
        }

        return string.Join(string.Empty, _value.Take(8));
    }

    private int GetNextValue(int number)
    {
        int result = 0;
        int countOfCurrentIndex;
        int currentIndex;

        if (number == 0)
        {
            countOfCurrentIndex = 0;
            currentIndex = 1;
        }
        else
        {
            countOfCurrentIndex = 1;
            currentIndex = 0;
        }

        for (int i = 0; i < _value.Length; i++)
        {
            result += _value[i] * _pattern[currentIndex];
            countOfCurrentIndex++;

            if (countOfCurrentIndex > number)
            {
                countOfCurrentIndex = 0;
                currentIndex = (currentIndex + 1) % _pattern.Length;
            }
        }

        return int.Abs(result % 10);
    }
}
