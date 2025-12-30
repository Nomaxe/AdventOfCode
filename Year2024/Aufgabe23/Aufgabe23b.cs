using AdventOfCode.Utils;
using System.Collections;

namespace AdventOfCode.Year2024;

internal class Aufgabe23b : IAufgabe
{
    private readonly Dictionary<string, HashSet<string>> _connections = [];
    private HashSet<HashElement> _largestConnection = [];

    public Aufgabe23b()
    {
        var input = Utilities.ReadInput(2024, 23);
        foreach (var line in input)
        {
            var split = line.Split('-');
            Add(split[0], split[1]);
            Add(split[1], split[0]);
            AddToLargestConnection(_largestConnection, split);
        }
    }

    public string Calc()
    {
        while (_largestConnection.Count > 1)
        {
            HashSet<HashElement> nextLargestConnection = [];

            foreach (var largestConnection in _largestConnection)
            {
                foreach (var connection in _connections[largestConnection[0]].Where(x => !largestConnection.Contains(x)))
                {
                    if (Check(largestConnection, connection))
                    {
                        AddToLargestConnection(nextLargestConnection, largestConnection.Append(connection));
                    }
                }
            }

            _largestConnection = [.. nextLargestConnection];
        }

        return _largestConnection.First().ToString();
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

    private static void AddToLargestConnection(HashSet<HashElement> largestConnection, IEnumerable<string> connection)
    {
        largestConnection.Add(new(connection));
    }

    private bool Check(HashElement keys, string value)
    {
        foreach (var key in keys)
        {
            if (!_connections.TryGetValue(key, out var list))
            {
                return false;
            }

            if (!list.Contains(value))
            {
                return false;
            }
        }

        return true;
    }

    private class HashElement : IEnumerable<string>
    {
        private readonly string[] _strings;

        public HashElement(IEnumerable<string> strings)
        {
            _strings = [.. strings.Order()];
        }

        public string this[int key] => _strings[key];

        public bool Contains(string s) => _strings.Contains(s);

        public IEnumerable<string> Append(string s) => _strings.Append(s);

        public override string ToString()
        {
            return string.Join(',', _strings);
        }

        public override int GetHashCode()
        {
            int hashcode = 0;

            foreach (var s in _strings)
            {
                hashcode = HashCode.Combine(hashcode, s);
            }

            return hashcode;
        }

        public override bool Equals(object? obj)
        {
            if (obj is HashElement other)
            {
                return _strings.SequenceEqual(other._strings);
            }

            return false;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var s in _strings)
            {
                yield return s;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
