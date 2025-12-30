using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2021, 8);
    }

    public string Calc()
    {
        int count = 0;

        foreach (var line in _input)
        {
            count += line[61..].Split(' ').Count(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7);
        }

        return count.ToString();
    }
}
