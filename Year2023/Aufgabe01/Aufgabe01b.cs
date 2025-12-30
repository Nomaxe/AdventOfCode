using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe01b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe01b()
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
        string currentValue = string.Empty;

        foreach (var character in input)
        {
            if (character >= '0' && character <= '9')
            {
                return character;
            }
            else
            {
                currentValue += character;

                if (currentValue.Contains("one"))
                {
                    return '1';
                }
                else if (currentValue.Contains("two"))
                {
                    return '2';
                }
                else if (currentValue.Contains("three"))
                {
                    return '3';
                }
                else if (currentValue.Contains("four"))
                {
                    return '4';
                }
                else if (currentValue.Contains("five"))
                {
                    return '5';
                }
                else if (currentValue.Contains("six"))
                {
                    return '6';
                }
                else if (currentValue.Contains("seven"))
                {
                    return '7';
                }
                else if (currentValue.Contains("eight"))
                {
                    return '8';
                }
                else if (currentValue.Contains("nine"))
                {
                    return '9';
                }
            }
        }

        throw new NotImplementedException();
    }

    private static char GetLastNumber(string input)
    {
        string currentValue = string.Empty;

        for (int i = input.Length - 1; i >= 0; i--)
        {
            if (input[i] >= '0' && input[i] <= '9')
            {
                return input[i];
            }
            else
            {
                currentValue = $"{input[i]}{currentValue}";
                if (currentValue.Contains("one"))
                {
                    return '1';
                }
                else if (currentValue.Contains("two"))
                {
                    return '2';
                }
                else if (currentValue.Contains("three"))
                {
                    return '3';
                }
                else if (currentValue.Contains("four"))
                {
                    return '4';
                }
                else if (currentValue.Contains("five"))
                {
                    return '5';
                }
                else if (currentValue.Contains("six"))
                {
                    return '6';
                }
                else if (currentValue.Contains("seven"))
                {
                    return '7';
                }
                else if (currentValue.Contains("eight"))
                {
                    return '8';
                }
                else if (currentValue.Contains("nine"))
                {
                    return '9';
                }
            }
        }

        throw new NotImplementedException();
    }
}
