using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe07 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe07()
    {
        _input = Utilities.ReadInput(2016, 7);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            if (Check(line))
            {
                result++;
            }
        }

        return result.ToString();
    }

    private static bool Check(string input)
    {
        bool isValid = true;
        bool hasPair = false;

        for (int i = 0; i < input.Length - 3; i++)
        {
            if (input[i] == '[')
            {
                isValid = false;
                continue;
            }
            else if (input[i] == ']')
            {
                isValid = true;
                continue;
            }

            if (input[i] != input[i + 1] && input[i] == input[i + 3] && input[i + 1] == input[i + 2])
            {
                if (!isValid)
                {
                    return false;
                }

                hasPair = true;
            }
        }

        return hasPair;
    }
}
