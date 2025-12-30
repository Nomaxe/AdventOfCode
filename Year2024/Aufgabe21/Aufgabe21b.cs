using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe21b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe21b()
    {
        _input = Utilities.ReadInput(2024, 21);
    }

    public string Calc()
    {
        ulong result = 0;

        foreach (var line in _input)
        {
            var counter = CalcInput1(line);

            for (int i = 0; i < 25; i++)
            {
                counter = CalcInput2(counter);
                var min = counter.Min(GetCount);
                counter = counter.Where(x => GetCount(x) == min).ToList();
            }

            result += ulong.Parse(NumberRegex().Match(line).Value) * counter.Min(GetCount);
        }

        return result.ToString();
    }

    private static List<LargeCounter<string>> CalcInput2(List<LargeCounter<string>> counter)
    {
        List<LargeCounter<string>> returnCounter = [];

        foreach (var counterLoop in counter)
        {
            returnCounter.AddRange(CalcInput2Single(counterLoop));
        }

        return returnCounter;
    }

    private static List<LargeCounter<string>> CalcInput2Single(LargeCounter<string> counter)
    {
        List<LargeCounter<string>> returnCounterList = [];
        returnCounterList.Add([]);

        foreach (var key in counter)
        {
            var path = GetCompleteStepsForInput2(key.Key);
            if (path.Count == 1)
            {
                var split = Split(path[0]);
                foreach (var returnCounter in returnCounterList)
                {
                    foreach (var item in split)
                    {
                        returnCounter.Add(item, key.Value);
                    }
                }
            }
            else
            {
                List<LargeCounter<string>> nextResultCounterList = new(returnCounterList.Count);
                foreach (var pathItem in path)
                {
                    var split = Split(path[0]);
                    foreach (var returnCounter in returnCounterList)
                    {
                        var counterClone = returnCounter.Clone();
                        nextResultCounterList.Add(counterClone);
                        foreach (var item in split)
                        {
                            counterClone.Add(item, key.Value);
                        }
                    }
                }
                returnCounterList = nextResultCounterList;
            }
        }

        return returnCounterList;
    }

    private static List<string> GetCompleteStepsForInput2(string path)
    {
        List<string> possibilites = [string.Empty];
        char current = 'A';

        foreach (var target in path)
        {
            List<string> nextPossibilities = [];
            var steps = GetStepsForInput2(current, target);
            foreach (var possibility in possibilites)
            {
                foreach (var step in steps)
                {
                    nextPossibilities.Add(possibility + step);
                }
            }
            possibilites = nextPossibilities;
            current = target;
        }

        return possibilites;
    }

    private static List<LargeCounter<string>> CalcInput1(string line)
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
                    nextPossibilities.Add(possibility + step);
                }
            }
            possibilities = nextPossibilities;
            current = target;
        }

        return possibilities.Select(x => new LargeCounter<string>(Split(x))).ToList();
    }

    private static List<string> GetStepsForInput1(char current, char target)
    {
        return current switch
        {
            '0' => target switch
            {
                '1' => ["^<A"],
                '2' => GetPath(1, 0),
                '3' => GetPath(1, 1),
                '4' => ["^^<A"],
                '5' => GetPath(2, 0),
                '6' => GetPath(2, 1),
                '7' => ["^^^<A"],
                '8' => GetPath(3, 0),
                '9' => GetPath(3, 1),
                'A' => GetPath(0, 1),
                _ => throw new NotImplementedException()
            },
            '1' => target switch
            {
                '0' => [">vA"],
                '2' => GetPath(0, 1),
                '3' => GetPath(0, 2),
                '4' => GetPath(1, 0),
                '5' => GetPath(1, 1),
                '6' => GetPath(1, 2),
                '7' => GetPath(2, 0),
                '8' => GetPath(2, 1),
                '9' => GetPath(2, 2),
                'A' => [">>vA"],
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
                '0' => [">vvA"],
                '1' => GetPath(-1, 0),
                '2' => GetPath(-1, 1),
                '3' => GetPath(-1, 2),
                '5' => GetPath(0, 1),
                '6' => GetPath(0, 2),
                '7' => GetPath(1, 0),
                '8' => GetPath(1, 1),
                '9' => GetPath(1, 2),
                'A' => [">>vvA"],
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
                '0' => [">vvvA"],
                '1' => GetPath(-2, 0),
                '2' => GetPath(-2, 1),
                '3' => GetPath(-2, 2),
                '4' => GetPath(-1, 0),
                '5' => GetPath(-1, 1),
                '6' => GetPath(-1, 2),
                '8' => GetPath(0, 1),
                '9' => GetPath(0, 2),
                'A' => [">>vvvA"],
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
                '1' => ["^<<A"],
                '2' => GetPath(1, -1),
                '3' => GetPath(1, 0),
                '4' => ["^^<<A"],
                '5' => GetPath(2, -1),
                '6' => GetPath(2, 0),
                '7' => ["<<^^^A"],
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
                '^' => ["A"],
                '>' => GetPath(-1, 1),
                'v' => GetPath(-1, 0),
                '<' => ["v<A"],
                'A' => GetPath(0, 1),
                _ => throw new NotImplementedException()
            },
            '>' => target switch
            {
                '^' => GetPath(1, -1),
                '>' => ["A"],
                'v' => GetPath(0, -1),
                '<' => GetPath(0, -2),
                'A' => GetPath(1, 0),
                _ => throw new NotImplementedException()
            },
            'v' => target switch
            {
                '^' => GetPath(1, 0),
                '>' => GetPath(0, 1),
                'v' => ["A"],
                '<' => GetPath(0, -1),
                'A' => GetPath(1, 1),
                _ => throw new NotImplementedException()
            },
            '<' => target switch
            {
                '^' => [">^A"],
                '>' => GetPath(0, 2),
                'v' => GetPath(0, 1),
                '<' => ["A"],
                'A' => [">>^A"],
                _ => throw new NotImplementedException()
            },
            'A' => target switch
            {
                '^' => GetPath(0, -1),
                '>' => GetPath(-1, 0),
                'v' => GetPath(-1, -1),
                '<' => ["v<<A"],
                'A' => ["A"],
                _ => throw new NotImplementedException()
            },
            _ => throw new NotImplementedException()
        };
    }

    private static List<string> GetPath(int up, int right)
    {
        return (up, right) switch
        {
            (-3, 0) => ["vvvA"],
            (-2, 0) => ["vvA"],
            (-2, 1) => ["vv>A"],
            (-1, -1) => ["<vA"],
            (-1, 0) => ["vA"],
            (-1, 1) => ["v>A"],
            (-1, 2) => ["v>>A"],
            (0, -2) => ["<<A"],
            (0, -1) => ["<A"],
            (0, 1) => [">A"],
            (0, 2) => [">>A"],
            (1, -2) => ["<<^A"],
            (1, -1) => ["<^A"],
            (1, 0) => ["^A"],
            (1, 1) => ["^>A"],
            (1, 2) => ["^>>A", ">>^A"],
            (2, -2) => ["^^<<A", "<<^^A"],
            (2, -1) => ["^^<A", "<^^A"],
            (2, 0) => ["^^A"],
            (2, 1) => ["^^>A", ">^^A"],
            (2, 2) => ["^^>>A", ">>^^A"],
            (3, -1) => ["<^^^A", "^^^<A"],
            (3, 0) => ["^^^A"],
            (3, 1) => [">^^^A", "^^^>A"],
            _ => throw new NotImplementedException()
        };
    }

    private static IEnumerable<string> Split(string path)
    {
        return path.Split('A')[..^1].Select(x => x + 'A');
    }

    public ulong GetCount(LargeCounter<string> counter)
    {
        ulong result = 0;

        foreach (var path in counter)
        {
            result += (ulong)path.Key.Length * path.Value;
        }

        return result;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
