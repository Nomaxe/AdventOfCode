using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2018;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<int, List<int>> _counter;

    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2018, 4);
        _counter = [];
    }

    public string Calc()
    {
        int currentId = 0;
        int fallAsleepTime = 0;

        Array.Sort(_input);

        foreach (var line in _input)
        {
            if (line[19] == 'G')
            {
                //neuer Guard
                var idString = line[26..];
                currentId = int.Parse(idString[..idString.IndexOf(' ')]);
                continue;
            }

            var time = int.Parse(line[15..17]);

            switch (line[19])
            {
                case 'f':
                    fallAsleepTime = time;
                    break;
                case 'w':
                    AddToCounter(currentId, fallAsleepTime, time);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        var maxId = _counter.MaxBy(x => x.Value.Count);
        var mostSleepMinute = maxId.Value.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
        return (maxId.Key * mostSleepMinute).ToString();
    }

    private void AddToCounter(int id, int from, int to)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_counter, id, out var exists);
        if (!exists)
        {
            list = [];
        }

        for (int i = from; i < to; i++)
        {
            list!.Add(i);
        }
    }
}
