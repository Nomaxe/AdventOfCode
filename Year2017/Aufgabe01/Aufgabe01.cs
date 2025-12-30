using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe01 : IAufgabe
{
    private readonly string _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2017, 1)[0];
    }

    public string Calc()
    {
        var lastNumber = _input[0].ToNumber();
        int result = 0;

        if (lastNumber == _input[^1].ToNumber())
        {
            result += lastNumber;
        }

        for (int i = 1; i < _input.Length; i++)
        {
            var currentNumber = _input[i].ToNumber();
            if (currentNumber == lastNumber)
            {
                result += currentNumber;
            }
            else
            {
                lastNumber = currentNumber;
            }
        }

        return result.ToString();
    }
}
