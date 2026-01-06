using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe15b : IAufgabe
{
    private readonly int[] _input;
    private readonly DictionaryList<int, int> _numbers;

    public Aufgabe15b()
    {
        _input = Utilities.ReadInputAsArray<int>(2020, 15, ',');
        _numbers = new();
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            _numbers.Add(_input[i], i + 1);
        }

        int lastNumber = _input[^1];
        for (int i = _input.Length + 1; i <= 30000000; i++)
        {
            if (_numbers.TryGetValue(lastNumber, out var list))
            {
                if (list.Count >= 2)
                {
                    if (list.Count == 3)
                    {
                        _numbers.RemoveFirstItem(lastNumber);
                    }
                    lastNumber = list[^1] - list[^2];
                }
                else
                {
                    //Zahl erst einmal gesagt
                    lastNumber = 0;
                }
            }
            else
            {
                //Neue Zahl
                lastNumber = 0;
            }

            _numbers.Add(lastNumber, i);
        }

        return lastNumber.ToString();
    }
}
