using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2020, 2);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            var hyphen = line.IndexOf('-');
            var whitespace = line.IndexOf(' ', hyphen + 1);

            var firstIndex = int.Parse(line[..hyphen]);
            var secondIndex = int.Parse(line[(hyphen + 1)..whitespace]);
            var character = line[whitespace + 1];
            var testString = line[(whitespace + 4)..];

            if (testString[firstIndex - 1] == character ^ testString[secondIndex - 1] == character)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
