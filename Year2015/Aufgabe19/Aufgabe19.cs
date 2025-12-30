using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe19 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe19()
    {
        _input = Utilities.ReadInput(2015, 19);
    }

    public string Calc()
    {
        HashSet<string> results = [];
        var molecule = _input[^1];

        foreach (var line in _input.SkipLast(2))
        {
            var split = line.Split(" => ");
            var count = split[0].Length;

            if (count == 1)
            {
                var character = split[0][0];

                for (int i = 0; i < molecule.Length; i++)
                {
                    if (molecule[i] == character)
                    {
                        results.Add($"{molecule[..i]}{split[1]}{molecule[(i + 1)..]}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < molecule.Length - count; i++)
                {
                    if (molecule[i..(i + count)] == split[0])
                    {
                        results.Add($"{molecule[..i]}{split[1]}{molecule[(i + count)..]}");
                    }
                }
            }
        }

        return results.Count.ToString();
    }
}
