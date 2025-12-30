using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2022;

internal class Aufgabe16b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, int> _pipes;
    private readonly Dictionary<(string, string), int> _lengths;
    private readonly Dictionary<string, int> _results;

    private const int Minutes = 26;

    public Aufgabe16b()
    {
        _input = Utilities.ReadInput(2022, 16);
        _pipes = new(_input.Length);
        _lengths = [];
        _results = [];
    }

    public string Calc()
    {
        DictionaryList<string, string> connectedPipesDict = new(_input.Length);

        foreach (var line in _input)
        {
            var pipe = line[6..8];
            var flow = line[23..].GetNumber();
            var index = line.LastIndexOf("valve");
            if (line[index + 5] == 's')
            {
                index++;
            }
            var connectedPipes = line[(index + 6)..].Split(", ");
            _pipes.Add(pipe, flow);
            connectedPipesDict.Add(pipe, connectedPipes);
        }

        List<string> goodPipes = [.. _pipes.Where(x => x.Value > 0).Select(x => x.Key)];
        CalcLengths(goodPipes.Append("AA"), connectedPipesDict);
        Calc(0, 0, "AA", goodPipes, []);

        int maxPressure = 0;
        foreach (var result in _results)
        {
            var split = result.Key.Split(';');

            foreach (var resultElephant in _results.Where(x => DoesNotContain(x.Key, split)))
            {
                maxPressure = int.Max(result.Value + resultElephant.Value, maxPressure);
            }
        }

        return maxPressure.ToString();
    }

    private void Calc(int minute, int pressure, string currentPipe, List<string> remainingPipes, SortedSet<string> path)
    {
        if (minute >= Minutes)
        {
            AddResult(path, pressure);
            return;
        }

        foreach (var remainingPipe in remainingPipes)
        {
            var length = _lengths[(currentPipe, remainingPipe)];
            var newMinute = minute + length + 1;
            var newPressure = pressure;
            List<string> remainingPipesNew = [.. remainingPipes];
            SortedSet<string> pathNew = [.. path];
            if (newMinute < Minutes)
            {
                newPressure += _pipes[remainingPipe] * (Minutes - newMinute);
                remainingPipesNew.Remove(remainingPipe);
                pathNew.Add(remainingPipe);
            }
            Calc(newMinute, newPressure, remainingPipe, remainingPipesNew, pathNew);
        }

        AddResult(path, pressure);
    }

    private void CalcLengths(IEnumerable<string> goodPipes, DictionaryList<string, string> pipes)
    {
        foreach (var start in goodPipes)
        {
            foreach (var end in goodPipes.Where(x => x != start))
            {
                _lengths.Add((start, end), Move(pipes, start, end));
            }
        }
    }

    private static int Move(DictionaryList<string, string> pipes, string start, string end)
    {
        HashSet<string> currentPipes = [start];
        int steps = 0;

        while (!currentPipes.Contains(end))
        {
            HashSet<string> nextPipes = [];
            foreach (var currentPipe in currentPipes)
            {
                nextPipes.AddRange(pipes[currentPipe]);
            }

            currentPipes = nextPipes;
            steps++;
        }

        return steps;
    }

    private void AddResult(SortedSet<string> path, int pressure)
    {
        var pathString = string.Join(';', path);

        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(_results, pathString, out _);

        if (pressure > value)
        {
            value = pressure;
        }
    }

    private static bool DoesNotContain(string path, string[] pathItems)
    {
        return pathItems.All(x => !path.Contains(x));
    }
}
