using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

internal partial class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08b()
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
            var converted = line.Replace("\\", "??").Replace("\"", "??");
            converted = ASCIINotation().Replace(converted, "?????");
            sumMemory += converted.Length + 2;
        }

        return (sumMemory - sumCode).ToString();
    }

    [GeneratedRegex(@"\\x[\da-f]{2}")]
    private static partial Regex ASCIINotation();
}
