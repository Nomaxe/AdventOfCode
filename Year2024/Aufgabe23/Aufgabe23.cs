using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe23 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, HashSet<string>> _connections;

    public Aufgabe23()
    {
        _input = Utilities.ReadInput(2024, 23);
        _connections = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split('-');
            Add(split[0], split[1]);
            Add(split[1], split[0]);
        }

        HashSet<(string a, string b, string c)> uniqueCombinations = [];

        foreach (var connection in _connections.Where(x => x.Key.StartsWith('t')))
        {
            var list = connection.Value.ToList();
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (_connections[list[i]].Contains(list[j]))
                    {
                        string[] strings = [connection.Key, list[i], list[j]];
                        strings = [.. strings.Order()];
                        uniqueCombinations.Add((strings[0], strings[1], strings[2]));
                    }
                }
            }
        }

        return uniqueCombinations.Count.ToString();
    }

    private void Add(string key, string value)
    {
        if (_connections.TryGetValue(key, out var list))
        {
            list.Add(value);
        }
        else
        {
            _connections.Add(key, [value]);
        }
    }
}
