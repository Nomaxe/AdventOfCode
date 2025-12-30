using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2018;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<int, LargeCounter<int>> _counter;

    public Aufgabe04b()
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

        int mostSleepId = 0;
        int mostSleepMinute = 0;
        ulong mostSleepAmount = 0;
        foreach (var item in _counter)
        {
            var mostSleep = item.Value.GetMax();
            if (mostSleep.Value > mostSleepAmount)
            {
                mostSleepId = item.Key;
                mostSleepMinute = mostSleep.Key;
                mostSleepAmount = mostSleep.Value;
            }
        }

        return (mostSleepId * mostSleepMinute).ToString();
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
