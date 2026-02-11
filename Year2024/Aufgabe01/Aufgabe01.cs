using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe01 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2024, 1);
    }

    public string Calc()
    {
        List<int> number1 = new(_input.Length);
        List<int> number2 = new(_input.Length);
        int result = 0;
        foreach (var line in _input)
        {
            var numbers = line.Split(' ');
            number1.Add(int.Parse(numbers[0]));
            number2.Add(int.Parse(numbers[3]));
        }

        number1 = [.. number1.Order()];
        number2 = [.. number2.Order()];

        for (int i = 0; i < _input.Length; i++)
        {
            result += Math.Abs(number1[i] - number2[i]);
        }

        return result.ToString();
    }
}
