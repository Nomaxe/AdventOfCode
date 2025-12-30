using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe01 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2023, 1);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            result += int.Parse($"{GetFirstNumber(line)}{GetLastNumber(line)}");
        }

        return result.ToString();
    }

    private static char GetFirstNumber(string input)
    {
        foreach (var character in input)
        {
            if (character >= '0' && character <= '9')
            {
                return character;
            }
        }

        throw new NotImplementedException();
    }

    private static char GetLastNumber(string input)
    {
        for (int i = input.Length - 1; i >= 0; i--)
        {
            if (input[i] >= '0' && input[i] <= '9')
            {
                return input[i];
            }
        }

        throw new NotImplementedException();
    }
}
