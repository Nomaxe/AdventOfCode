using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe21 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe21()
    {
        _input = Utilities.ReadInput(2024, 21);
    }

    public string Calc()
    {
        ulong result = 0;

        foreach (var line in _input)
        {
            char current = 'A';
            List<string> possibilities = [string.Empty];

            foreach (var target in line)
            {
                List<string> nextPossibilities = [];
                var steps = GetStepsForInput1(current, target);
                foreach (var possibility in possibilities)
                {
                    foreach (var step in steps)
                    {
                        nextPossibilities.Add(possibility + step + 'A');
                    }
                }
                possibilities = nextPossibilities;
                current = target;
            }

            possibilities = GetPossibilitiesForInput2(possibilities);
            possibilities = GetPossibilitiesForInput2(possibilities);
            ulong resultThisLine = ulong.Parse(NumberRegex().Match(line).Value) * (ulong)possibilities.Min(x => x.Length);
            result += resultThisLine;
        }

        return result.ToString();
    }

    private static List<string> GetPossibilitiesForInput2(List<string> possibilitiesFormerStep)
    {
        List<string> returnPossibilities = [];

        foreach (var possibilityFormerStep in possibilitiesFormerStep)
        {
            List<string> possibilitesThisLoop = [string.Empty];
            char current = 'A';

            foreach (var target in possibilityFormerStep)
            {
                List<string> nextPossibilities = [];
                var steps = GetStepsForInput2(current, target);
                foreach (var possibility in possibilitesThisLoop)
                {
                    foreach (var step in steps)
                    {
                        nextPossibilities.Add(possibility + step + 'A');
                    }
                }
                possibilitesThisLoop = nextPossibilities;
                current = target;
            }

            returnPossibilities.AddRange(possibilitesThisLoop);
        }

        return returnPossibilities;
    }

    private static List<string> GetStepsForInput1(char current, char target)
    {
        return current switch
        {
            '0' => target switch
            {
                '1' => ["^<"],
                '2' => GetPath(1, 0),
                '3' => GetPath(1, 1),
                '4' => ["^<^", "^^<"],
                '5' => GetPath(2, 0),
                '6' => GetPath(2, 1),
                '7' => ["^^^<"],
                '8' => GetPath(3, 0),
                '9' => GetPath(3, 1),
                'A' => GetPath(0, 1),
                _ => throw new NotImplementedException()
            },
            '1' => target switch
            {
                '0' => [">v"],
                '2' => GetPath(0, 1),
                '3' => GetPath(0, 2),
                '4' => GetPath(1, 0),
                '5' => GetPath(1, 1),
                '6' => GetPath(1, 2),
                '7' => GetPath(2, 0),
                '8' => GetPath(2, 1),
                '9' => GetPath(2, 2),
                'A' => [">>v"],
                _ => throw new NotImplementedException()
            },
            '2' => target switch
            {
                '0' => GetPath(-1, 0),
                '1' => GetPath(0, -1),
                '3' => GetPath(0, 1),
                '4' => GetPath(1, -1),
                '5' => GetPath(1, 0),
                '6' => GetPath(1, 1),
                '7' => GetPath(2, -1),
                '8' => GetPath(2, 0),
                '9' => GetPath(2, 1),
                'A' => GetPath(-1, 1),
                _ => throw new NotImplementedException()
            },
            '3' => target switch
            {
                '0' => GetPath(-1, -1),
                '1' => GetPath(0, -2),
                '2' => GetPath(0, -1),
                '4' => GetPath(1, -2),
                '5' => GetPath(-1, -1),
                '6' => GetPath(1, 0),
                '7' => GetPath(2, -2),
                '8' => GetPath(2, -1),
                '9' => GetPath(2, 0),
                'A' => GetPath(-1, -1),
                _ => throw new NotImplementedException()
            },
            '4' => target switch
            {
                '0' => [">vv"],
                '1' => GetPath(-1, 0),
                '2' => GetPath(-1, 1),
                '3' => GetPath(-1, 2),
                '5' => GetPath(0, 1),
                '6' => GetPath(0, 2),
                '7' => GetPath(1, 0),
                '8' => GetPath(1, 1),
                '9' => GetPath(1, 2),
                'A' => [">>vv"],
                _ => throw new NotImplementedException()
            },
            '5' => target switch
            {
                '0' => GetPath(-2, 0),
                '1' => GetPath(-1, -1),
                '2' => GetPath(-1, 0),
                '3' => GetPath(-1, 1),
                '4' => GetPath(0, -1),
                '6' => GetPath(0, 1),
                '7' => GetPath(1, -1),
                '8' => GetPath(1, 0),
                '9' => GetPath(1, 1),
                'A' => GetPath(-2, 1),
                _ => throw new NotImplementedException()
            },
            '6' => target switch
            {
                '0' => GetPath(-2, -1),
                '1' => GetPath(-1, -2),
                '2' => GetPath(-1, -1),
                '3' => GetPath(-1, 0),
                '4' => GetPath(0, -2),
                '5' => GetPath(0, -1),
                '7' => GetPath(1, -2),
                '8' => GetPath(1, -1),
                '9' => GetPath(1, 0),
                'A' => GetPath(-2, 0),
                _ => throw new NotImplementedException()
            },
            '7' => target switch
            {
                '0' => [">vvv"],
                '1' => GetPath(-2, 0),
                '2' => GetPath(-2, 1),
                '3' => GetPath(-2, 2),
                '4' => GetPath(-1, 0),
                '5' => GetPath(-1, 1),
                '6' => GetPath(-1, 2),
                '8' => GetPath(0, 1),
                '9' => GetPath(0, 2),
                'A' => [">>vvv"],
                _ => throw new NotImplementedException()
            },
            '8' => target switch
            {
                '0' => GetPath(-3, 0),
                '1' => GetPath(-2, -1),
                '2' => GetPath(-2, 0),
                '3' => GetPath(-2, 1),
                '4' => GetPath(-1, -1),
                '5' => GetPath(-1, 0),
                '6' => GetPath(-1, 1),
                '7' => GetPath(0, -1),
                '9' => GetPath(0, 1),
                'A' => GetPath(-3, 1),
                _ => throw new NotImplementedException()
            },
            '9' => target switch
            {
                '0' => GetPath(-3, -1),
                '1' => GetPath(-2, -2),
                '2' => GetPath(-2, -1),
                '3' => GetPath(-2, 0),
                '4' => GetPath(-1, -2),
                '5' => GetPath(-1, 1),
                '6' => GetPath(-1, 0),
                '7' => GetPath(0, -2),
                '8' => GetPath(0, -1),
                'A' => GetPath(-3, 0),
                _ => throw new NotImplementedException()
            },
            'A' => target switch
            {
                '0' => GetPath(0, -1),
                '1' => ["^<<"],
                '2' => GetPath(1, -1),
                '3' => GetPath(1, 0),
                '4' => ["^^<<"],
                '5' => GetPath(2, -1),
                '6' => GetPath(2, 0),
                '7' => ["<<^^^"],
                '8' => GetPath(3, -1),
                '9' => GetPath(3, 0),
                _ => throw new NotImplementedException()
            },
            _ => throw new NotImplementedException()
        };
    }

    private static List<string> GetStepsForInput2(char current, char target)
    {
        return current switch
        {
            '^' => target switch
            {
                '^' => [string.Empty],
                '>' => GetPath(-1, 1),
                'v' => GetPath(-1, 0),
                '<' => ["v<"],
                'A' => GetPath(0, 1),
                _ => throw new NotImplementedException()
            },
            '>' => target switch
            {
                '^' => GetPath(1, -1),
                '>' => [string.Empty],
                'v' => GetPath(0, -1),
                '<' => GetPath(0, -2),
                'A' => GetPath(1, 0),
                _ => throw new NotImplementedException()
            },
            'v' => target switch
            {
                '^' => GetPath(1, 0),
                '>' => GetPath(0, 1),
                'v' => [string.Empty],
                '<' => GetPath(0, -1),
                'A' => GetPath(1, 1),
                _ => throw new NotImplementedException()
            },
            '<' => target switch
            {
                '^' => [">^"],
                '>' => GetPath(0, 2),
                'v' => GetPath(0, 1),
                '<' => [string.Empty],
                'A' => [">>^", ">^>"],
                _ => throw new NotImplementedException()
            },
            'A' => target switch
            {
                '^' => GetPath(0, -1),
                '>' => GetPath(-1, 0),
                'v' => GetPath(-1, -1),
                '<' => ["v<<", "<v<"],
                'A' => [string.Empty],
                _ => throw new NotImplementedException()
            },
            _ => throw new NotImplementedException()
        };
    }

    private static List<string> GetPath(int up, int right)
    {
        return (up, right) switch
        {
            (-3, 0) => ["vvv"],
            (-2, 0) => ["vv"],
            (-2, 1) => ["vv>"],
            (-1, -1) => ["<v"],
            (-1, 0) => ["v"],
            (-1, 1) => ["v>"],
            (-1, 2) => ["v>>"],
            (0, -2) => ["<<"],
            (0, -1) => ["<"],
            (0, 1) => [">"],
            (0, 2) => [">>"],
            (1, -2) => ["<<^"],
            (1, -1) => ["<^"],
            (1, 0) => ["^"],
            (1, 1) => ["^>", ">^"],
            (1, 2) => ["^>>", ">>^"],
            (2, -2) => ["^^<<", "<<^^"],
            (2, -1) => ["^^<", "<^^"],
            (2, 0) => ["^^"],
            (2, 1) => ["^^>", ">^^"],
            (2, 2) => ["^^>>", ">>^^"],
            (3, -1) => ["<^^^", "^^^<"],
            (3, 0) => ["^^^"],
            (3, 1) => [">^^^", "^^^>"],
            _ => throw new NotImplementedException()
        };
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
