using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe01b : IAufgabe
{
    public string Calc()
    {
        var input = Utilities.ReadInput(2024, 1);
        List<int> number1 = new(input.Length);
        LargeCounter<int> number2 = [];
        int result = 0;
        foreach (var line in input)
        {
            var numbers = line.Split(' ');
            number1.Add(int.Parse(numbers[0]));
            number2.Add(int.Parse(numbers[3]));
        }

        foreach (var number in number1)
        {
            result += number * (int)number2.GetValueOrDefault(number);
        }

        return result.ToString();
    }
}
