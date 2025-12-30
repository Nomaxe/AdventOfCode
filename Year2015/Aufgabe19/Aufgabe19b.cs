using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

internal partial class Aufgabe19b : IAufgabe
{
    private readonly string _molecule;

    public Aufgabe19b()
    {
        _molecule = Utilities.ReadInput(2015, 19)[^1];
    }

    public string Calc()
    {
        //https://www.reddit.com/r/adventofcode/comments/3xflz8/comment/cy4etju/
        int count = 0;

        //alle Elemente starten mit Großbuchstaben und sind danach klein
        count = _molecule.Count(char.IsAsciiLetterUpper);
        count -= RegexRn().Count(_molecule);
        count -= RegexAr().Count(_molecule);
        count -= RegexY().Count(_molecule) * 2;

        count--;

        return count.ToString();
    }

    [GeneratedRegex("Rn")]
    private static partial Regex RegexRn();
    [GeneratedRegex("Ar")]
    private static partial Regex RegexAr();
    [GeneratedRegex("Y")]
    private static partial Regex RegexY();
}
