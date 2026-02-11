using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2024, 3);
    }

    public string Calc()
    {
        long resultNumber = 0;
        var line = string.Concat(_input);

        Regex regex = RegexMultiplicator();
        Regex numberRegex = RegexNumber();

        do
        {
            var nextChangeToDont = line.IndexOf("don't()");
            var lineToCheck = line[..nextChangeToDont];

            foreach (Match result in regex.Matches(lineToCheck))
            {
                var numbers = numberRegex.Matches(result.Value);
                resultNumber += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
            }

            line = line[nextChangeToDont..];
            var nextChangeToDo = line.IndexOf("do()");
            if (nextChangeToDo == -1)
            {
                break;
            }
            line = line[nextChangeToDo..];

        } while (!string.IsNullOrWhiteSpace(line));

        return resultNumber.ToString();
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex RegexMultiplicator();
    [GeneratedRegex(@"\d+")]
    private static partial Regex RegexNumber();
}
