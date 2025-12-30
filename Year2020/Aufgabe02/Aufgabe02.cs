using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
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

            var min = int.Parse(line[..hyphen]);
            var max = int.Parse(line[(hyphen + 1)..whitespace]);
            var character = line[whitespace + 1];
            var testString = line[(whitespace + 4)..];

            var count = testString.Count(x => x == character);
            if (count >= min && count <= max)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
