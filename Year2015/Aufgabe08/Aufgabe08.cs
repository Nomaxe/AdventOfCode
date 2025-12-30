using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

internal partial class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2015, 8);
    }

    public string Calc()
    {
        int sumCode = 0;
        int sumMemory = 0;

        foreach (var line in _input)
        {
            sumCode += line.Length;
            var converted = line[1..^1].Replace("\\\\", "?").Replace("\\\"", "?");
            converted = ASCIINotation().Replace(converted, "?");
            sumMemory += converted.Length;
        }

        return (sumCode - sumMemory).ToString();
    }

    [GeneratedRegex(@"\\x[\da-f]{2}")]
    private static partial Regex ASCIINotation();
}
