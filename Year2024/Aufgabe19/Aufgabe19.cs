using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe19 : IAufgabe
{
    private readonly Dictionary<char, List<string>> _towels = [];
    private readonly List<string> _designs = [];
    private readonly HashSet<string> _notPossible = [];

    public Aufgabe19()
    {
        var whiteline = false;
        var input = Utilities.ReadInput(2024, 19);

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                whiteline = true;
                continue;
            }

            if (!whiteline)
            {
                foreach (var split in line.Split(", "))
                {
                    if (_towels.TryGetValue(split[0], out var list))
                    {
                        list.Add(split);
                    }
                    else
                    {
                        _towels.Add(split[0], [split]);
                    }
                }
            }
            else
            {
                _designs.Add(line);
            }
        }
    }

    public string Calc()
    {
        int result = 0;

        foreach (var design in _designs)
        {
            if (CheckDesign(design))
            {
                result++;
            }
        }

        return result.ToString();
    }

    private bool CheckDesign(string design)
    {
        if (!_towels.TryGetValue(design[0], out List<string>? value))
        {
            return false;
        }

        foreach (var towel in value)
        {
            if (CheckTowel(design, towel))
            {
                var remaining = design[towel.Length..];

                if (string.IsNullOrEmpty(remaining))
                {
                    return true;
                }

                if (_notPossible.Contains(remaining))
                {
                    continue;
                }

                if (CheckDesign(remaining))
                {
                    return true;
                }
            }
        }

        _notPossible.Add(design);
        return false;
    }

    private static bool CheckTowel(string design, string towel)
    {
        if (towel.Length > design.Length)
        {
            return false;
        }

        return towel == design[..towel.Length];
    }
}
