using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe19b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<char, List<string>> _towels = [];
    private readonly List<string> _designs = [];
    private readonly HashSet<string> _notPossible = [];
    private readonly SortedDictionary<string, ulong> _possible = [];

    public Aufgabe19b()
    {
        
        _input = Utilities.ReadInput(2024, 19);
    }

    public string Calc()
    {
        var whiteline = false;

        foreach (var line in _input)
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

        ulong result = 0;

        foreach (var design in _designs)
        {
            result += CheckDesign(design);
        }

        return result.ToString();
    }

    private ulong CheckDesign(string design)
    {
        if (_possible.TryGetValue(design, out var result))
        {
            return result;
        }

        if (!_towels.TryGetValue(design[0], out List<string>? value))
        {
            return 0;
        }

        foreach (var towel in value)
        {
            if (CheckTowel(design, towel))
            {
                var remaining = design[towel.Length..];

                if (string.IsNullOrEmpty(remaining))
                {
                    result++;
                    continue;
                }

                if (_notPossible.Contains(remaining))
                {
                    continue;
                }

                result += CheckDesign(remaining);
            }
        }

        if (result == 0)
        {
            _notPossible.Add(design);
        }
        else
        {
            _possible.Add(design, result);
        }
        return result;
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
