using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

internal partial class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2015, 5);
    }

    public string Calc()
    {
        int niceStrings = 0;

        foreach (var line in _input)
        {
            if (VowelCount().Count(line) < 3)
            {
                continue;
            }

            if (!Duplicate().IsMatch(line))
            {
                continue;
            }

            if (NaughtyStrings().IsMatch(line))
            {
                continue;
            }

            niceStrings++;
        }

        return niceStrings.ToString();
    }

    [GeneratedRegex(@"[aeiou]")]
    private static partial Regex VowelCount();
    [GeneratedRegex(@"([a-z])\1")]
    private static partial Regex Duplicate();
    [GeneratedRegex(@"(ab|cd|pq|xy)")]
    private static partial Regex NaughtyStrings();
}
