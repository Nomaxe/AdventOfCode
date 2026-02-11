using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe03 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2024, 3);
    }

    public string Calc()
    {
        long resultNumber = 0;
        var line = string.Concat(_input);
        Regex regex = RegexMultiplicator();
        var results = regex.Matches(line);

        Regex numberRegex = RegexNumber();
        foreach (Match result in results)
        {
            var numbers = numberRegex.Matches(result.Value);
            resultNumber += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }

        return resultNumber.ToString();
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex RegexMultiplicator();
    [GeneratedRegex(@"\d+")]
    private static partial Regex RegexNumber();
}
