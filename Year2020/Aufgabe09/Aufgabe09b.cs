using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe09b : IAufgabe
{
    private readonly List<long> _input;
    private readonly HashSet<long> _numbers;

    public Aufgabe09b()
    {
        _input = Utilities.ReadInputAsList<long>(2020, 9);
        _numbers = new(25);
    }

    public string Calc()
    {
        for (int i = 0; i < 25; i++)
        {
            _numbers.Add(_input[i]);
        }

        for (int i = 25; i < _input.Count; i++)
        {
            if (!IsCorrectNumber(_input[i]))
            {
                return GetResultNumber(i).ToString();
            }

            _numbers.Remove(_input[i - 25]);
            _numbers.Add(_input[i]);
        }

        throw new NotImplementedException();
    }

    private bool IsCorrectNumber(long result)
    {
        foreach (var number in _numbers)
        {
            if (_numbers.Contains(result - number))
            {
                return true;
            }
        }

        return false;
    }

    private long GetResultNumber(int index)
    {
        var numberToFind = _input[index];

        for (int i = 0; i < index; i++)
        {
            int endIndex = i;
            long number = _input[i];

            while (number < numberToFind)
            {
                endIndex++;
                number += _input[endIndex];
            }

            if (number == numberToFind)
            {
                long smallest = long.MaxValue;
                long biggest = 0;

                for (int j = i; j <= endIndex; j++)
                {
                    if (_input[j] < smallest)
                    {
                        smallest = _input[j];
                    }
                    if (_input[j] > biggest)
                    {
                        biggest = _input[j];
                    }
                }

                return smallest + biggest;
            }
        }

        throw new NotImplementedException();
    }
}
