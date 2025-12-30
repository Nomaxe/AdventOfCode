using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal class Aufgabe03b : IAufgabe
{
    public string Calc()
    {
        long resultNumber = 0;
        var input = Utilities.ReadInput(2024, 3);
        var line = string.Concat(input);

        Regex regex = new(@"mul\(\d{1,3},\d{1,3}\)");
        Regex numberRegex = new(@"\d+");

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
}
