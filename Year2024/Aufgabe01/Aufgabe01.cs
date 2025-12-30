using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe01 : IAufgabe
{
    public string Calc()
    {
        var input = Utilities.ReadInput(2024, 1);
        List<int> number1 = new(input.Length);
        List<int> number2 = new(input.Length);
        int result = 0;
        foreach (var line in input)
        {
            var numbers = line.Split(' ');
            number1.Add(int.Parse(numbers[0]));
            number2.Add(int.Parse(numbers[3]));
        }

        number1 = [.. number1.Order()];
        number2 = [.. number2.Order()];

        for (int i = 0; i < input.Length; i++)
        {
            result += Math.Abs(number1[i] - number2[i]);
        }

        return result.ToString();
    }
}
