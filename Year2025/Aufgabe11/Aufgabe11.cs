using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe11 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _connections;
    private readonly DictionaryCounter<string> _counter;

    public Aufgabe11()
    {
        _input = Utilities.ReadInput(2025, 11);
        _connections = new(_input.Length);
        _counter = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var key = line[..3];
            var split = line[5..].Split(' ').Where(x => x != "out");
            _connections.Add(key, split);
        }

        Queue<string> queue = new();
        Queue<int> queueInt = new();
        queue.Enqueue("you");
        queueInt.Enqueue(1);

        while (queue.Count > 0)
        {
            var key = queue.Dequeue();
            var count = queueInt.Dequeue();

            foreach (var nextKey in _connections[key])
            {
                queue.Enqueue(nextKey);
                queueInt.Enqueue(count);
                _counter.Add(nextKey, count);
            }
        }

        var result = 0;

        foreach (var outKeys in _connections.Where(x => x.Value.Count == 0))
        {
            result += _counter.GetValueOrDefault(outKeys.Key);
        }

        return result.ToString();
    }
}
