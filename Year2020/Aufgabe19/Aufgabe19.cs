using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe19 : IAufgabe
{
    private readonly Dictionary<int, List<string>> _rules = [];
    private readonly List<string> _checkStrings = [];

    public Aufgabe19()
    {
        var input = Utilities.ReadInput(2020, 19);
        bool whiteline = false;
        List<(int Id, List<List<int>> Rules)> remaining = [];
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                whiteline = true;
                continue;
            }

            if (!whiteline)
            {
                if (line.Contains('"'))
                {
                    _rules.Add(int.Parse(line[..line.IndexOf(':')]), [$"{line[^2]}"]);
                }
                else
                {
                    int colon = line.IndexOf(':');
                    int id = int.Parse(line[..colon]);
                    int pipe = line.IndexOf("|");
                    List<List<int>> rules = [];
                    if (pipe < 0)
                    {
                        rules.Add(GetRules(line[(colon + 2)..]));
                    }
                    else
                    {
                        rules.Add(GetRules(line[(colon + 2)..(pipe - 1)]));
                        rules.Add(GetRules(line[(pipe + 2)..]));
                    }

                    remaining.Add((id, rules));
                }
            }
            else
            {
                _checkStrings.Add(line);
            }
        }

        while (remaining.Count > 0)
        {
            List<(int Id, List<List<int>>)> nextRemaining = [];

            foreach (var rule in remaining)
            {
                if (!AllChildRulesExists(rule.Rules))
                {
                    nextRemaining.Add(rule);
                    continue;
                }

                var list = CreateRule(rule.Rules[0]);
                if (rule.Rules.Count == 2)
                {
                    list.AddRange(CreateRule(rule.Rules[1]));
                }

                _rules.Add(rule.Id, list);
            }

            remaining = nextRemaining;
        }
    }

    public string Calc()
    {
        int result = 0;
        var hashset = _rules[0].ToHashSet();

        foreach (var checkString in _checkStrings)
        {
            if (hashset.Contains(checkString))
            {
                result++;
            }
        }

        return result.ToString();
    }

    private static List<int> GetRules(string input)
    {
        var split = input.Split(' ');
        List<int> list = [];
        foreach (var s in split)
        {
            list.Add(int.Parse(s));
        }
        return list;
    }

    private bool AllChildRulesExists(List<List<int>> rules)
    {
        foreach (var rule in rules)
        {
            foreach (var id in rule)
            {
                if (!_rules.ContainsKey(id))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private List<string> CreateRule(List<int> rules)
    {
        List<string> list = [string.Empty];

        foreach (var rule in rules)
        {
            List<string> nextList = [];
            var strings = _rules[rule];
            foreach (var l in list)
            {
                foreach (var s in strings)
                {
                    nextList.Add(l + s);
                }
            }

            list = nextList;
        }

        return list;
    }
}
