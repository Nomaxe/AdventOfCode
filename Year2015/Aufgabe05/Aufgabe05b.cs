using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal partial class Aufgabe05b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2015, 5);
    }

    public string Calc()
    {
        int niceStrings = 0;

        foreach (var line in _input)
        {
            if (!HasDuplicate(line))
            {
                continue;
            }

            if (!HasCharRepeating(line))
            {
                continue;
            }

            niceStrings++;
        }

        return niceStrings.ToString();
    }

    private static bool HasDuplicate(string input)
    {
        for (int i = 0; i < input.Length - 3; i++)
        {
            var test = input[i..(i + 2)];
            if (input[(i + 2)..].Contains(test))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasCharRepeating(string input)
    {
        for (int i = 0; i < input.Length - 2; i++)
        {
            if (input[i] == input[i + 2])
            {
                return true;
            }
        }

        return false;
    }
}
