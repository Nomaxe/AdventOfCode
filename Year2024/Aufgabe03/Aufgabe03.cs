using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal class Aufgabe03 : IAufgabe
{
    public string Calc()
    {
        long resultNumber = 0;
        var input = Utilities.ReadInput(2024, 3);
        var line = string.Concat(input);
        Regex regex = new(@"mul\(\d{1,3},\d{1,3}\)");
        var results = regex.Matches(line);

        Regex numberRegex = new(@"\d+");
        foreach (Match result in results)
        {
            var numbers = numberRegex.Matches(result.Value);
            resultNumber += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }

        return resultNumber.ToString();
    }
}
