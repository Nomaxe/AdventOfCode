using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe11b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _connections;
    private readonly LargeCounter<string> _counter;

    public Aufgabe11b()
    {
        _input = Utilities.ReadInput(2025, 11);
        _connections = new(_input.Length);
        _counter = new();
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var key = line[..3];
            var split = line[5..].Split(' ');
            _connections.Add(key, split);
        }

        var firstPathA = GetPathCount("svr", "dac", "fft");
        _counter.Clear();
        var secondPathA = GetPathCount("dac", "fft");
        _counter.Clear();
        var thirdPathA = GetPathCount("fft", "out");

        _counter.Clear();
        var firstPathB = GetPathCount("svr", "fft", "dac");
        _counter.Clear();
        var secondPathB = GetPathCount("fft", "dac");
        _counter.Clear();
        var thirdPathB = GetPathCount("dac", "out");

        return (firstPathA * secondPathA * thirdPathA + firstPathB * secondPathB * thirdPathB).ToString();
    }

    private ulong GetPathCount(string start, string end, string avoid = "")
    {
        if (start == end)
        {
            return 1;
        }

        if (start == "out")
        {
            //Ende erreicht, aber nicht gesucht, da nicht im ersten if
            return 0;
        }

        if (_counter.TryGetValue(start, out var value))
        {
            return value;
        }

        ulong result = 0;

        foreach (var nextKey in _connections[start])
        {
            if (nextKey != avoid)
            {
                result += GetPathCount(nextKey, end, avoid);
            }
        }

        _counter.Add(start, result);

        return result;
    }
}
