using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe07b()
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
        List<string> pairsInABA = [];
        List<string> pairsInBAB = [];
        bool inABA = true;

        for (int i = 0; i <= input.Length - 3; i++)
        {
            if (input[i] == '[')
            {
                inABA = false;
                continue;
            }
            else if (input[i] == ']')
            {
                inABA = true;
                continue;
            }

            if (input[i] == input[i + 2] && input[i] != input[i + 1] && input[i + 1] != '[' && input[i + 1] != ']')
            {
                if (inABA)
                {
                    pairsInABA.Add(input[i..(i + 3)]);
                }
                else
                {
                    pairsInBAB.Add(input[i..(i + 3)]);
                }
            }
        }

        foreach (var pairInABA in pairsInABA)
        {
            if (pairsInBAB.Any(x => x[0] == pairInABA[1] && x[1] == pairInABA[0]))
            {
                return true;
            }
        }

        return false;
    }
}
