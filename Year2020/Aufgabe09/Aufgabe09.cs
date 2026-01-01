using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe09 : IAufgabe
{
    private readonly List<long> _input;
    private readonly HashSet<long> _numbers;

    public Aufgabe09()
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
                return _input[i].ToString();
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
}
