using AdventOfCode.Utils;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020;

internal class Aufgabe19b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<int, List<int>> _rules;
    private readonly Dictionary<int, char> _character;

    public Aufgabe19b()
    {
        _input = Utilities.ReadInput(2020, 19);
        _rules = new();
        _character = new(2);
    }

    public string Calc()
    {
        bool rules = true;
        Regex? regex = null;
        int result = 0;

        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                rules = false;
                regex = new($"^{BuildRegex(0)}$");
                continue; 
            }

            if (rules)
            {
                int colon = line.IndexOf(':');
                int id = int.Parse(line[..colon]);

                if (line.Contains('"'))
                {
                    _character.Add(id, line[^2]);
                    continue;
                }

                int pipe = line.IndexOf('|');
                if (pipe < 0)
                {
                    _rules.Add(id, GetRules(line[(colon + 2)..]));
                }
                else
                {
                    _rules.Add(id, GetRules(line[(colon + 2)..(pipe - 1)]));
                    _rules.Add(id, GetRules(line[(pipe + 2)..]));
                }

                continue;
            }

            if (regex!.IsMatch(line))
            {
                result++;
            }
        }

        return result.ToString();
    }

    private static List<int> GetRules(string input)
    {
        return input.GetUnsignedNumbers().ToList();
    }

    private string BuildRegex(int id)
    {
        if (_character.TryGetValue(id, out var character))
        {
            return $"{character}";
        }

        var rules = _rules[id];
        StringBuilder builder = new();

        if (id == 11)
        {
            var rule42 = BuildRegex(42);
            var rule31 = BuildRegex(31);

            builder.Append('(');
            builder.Append($"{rule42}{rule31}");
            builder.Append($"|{rule42}{rule42}{rule31}{rule31}");
            builder.Append($"|{rule42}{rule42}{rule42}{rule31}{rule31}{rule31}");
            builder.Append($"|{rule42}{rule42}{rule42}{rule42}{rule31}{rule31}{rule31}{rule31}");
            builder.Append(')');
            return builder.ToString();
        }


        if (rules.Count > 1 || id == 8)
        {
            builder.Append('(');
        }

        bool first = true;
        foreach (var ruleList in rules)
        {
            if (!first)
            {
                builder.Append('|');
            }

            foreach (var rule in ruleList)
            {
                builder.Append(BuildRegex(rule));
            }

            first = false;
        }

        if (rules.Count > 1 || id == 8)
        {
            builder.Append(')');
        }

        if (id == 8)
        {
            builder.Append('+');
        }
        return builder.ToString();
    }
}
