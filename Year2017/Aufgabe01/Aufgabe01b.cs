using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe01b : IAufgabe
{
    private readonly string _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInput(2017, 1)[0];
    }

    public string Calc()
    {
        int result = 0;
        var jumpAmount = _input.Length / 2;

        for (int i = 0; i < jumpAmount; i++)
        {
            var number1 = _input[i].ToNumber();
            var number2 = _input[i + jumpAmount].ToNumber();

            if (number1 == number2)
            {
                result += 2 * number1;
            }
        }

        return result.ToString();
    }
}
