using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe18b : IAufgabe
{
    private readonly string[] _input;
    private ulong _result = 0;

    public Aufgabe18b()
    {
        _input = Utilities.ReadInput(2020, 18);
    }

    public string Calc()
    {
        foreach (var input in _input)
        {
            _result += Calc(input);
        }

        return _result.ToString();
    }

    private static ulong Calc(string input)
    {
        List<ulong> numbers = [];
        List<char> signs = [];

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                numbers.Add((ulong)(input[i] - '0'));
            }
            else if (input[i] == '+' || input[i] == '*')
            {
                signs.Add(input[i]);
            }
            else if (input[i] == '(')
            {
                var newI = GetEndIndexParentheses(input[i..]);
                numbers.Add(Calc(input[(i + 1)..(newI + i)]));
                i += newI + 1;
            }
        }

        while (numbers.Count > 1)
        {
            var index = signs.IndexOf('+');
            if (index < 0)
            {
                index = 0;
            }

            if (signs[index] == '+')
            {
                numbers[index] += numbers[index + 1];
            }
            else
            {
                numbers[index] *= numbers[index + 1];
            }

            numbers.RemoveAt(index + 1);
            signs.RemoveAt(index);
        }

        return numbers[0];
    }

    private static int GetEndIndexParentheses(string input)
    {
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                count++;
            }
            else if (input[i] == ')')
            {
                count--;
                if (count == 0)
                {
                    return i;
                }
            }
        }

        throw new NotImplementedException();
    }
}
