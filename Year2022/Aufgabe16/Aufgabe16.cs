using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe16 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, int> _pipes;
    private readonly Dictionary<(string, string), int> _lengths;
    private int _maxPressure;

    private const int Minutes = 30;

    public Aufgabe16()
    {
        _input = Utilities.ReadInput(2022, 16);
        _pipes = new(_input.Length);
        _lengths = [];
        _maxPressure = 0;
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
        Calc(0, 0, "AA", goodPipes);

        return _maxPressure.ToString();
    }

    private void Calc(int minute, int pressure, string currentPipe, List<string> remainingPipes)
    {
        if (minute >= Minutes)
        {
            _maxPressure = int.Max(_maxPressure, pressure);
            return;
        }

        foreach (var remainingPipe in remainingPipes)
        {
            var length = _lengths[(currentPipe, remainingPipe)];
            var newMinute = minute + length + 1;
            var newPressure = pressure;
            if (newMinute < Minutes)
            {
                newPressure += _pipes[remainingPipe] * (Minutes - newMinute);
            }
            Calc(newMinute, newPressure, remainingPipe, [.. remainingPipes.Where(x => x != remainingPipe)]);
        }

        _maxPressure = int.Max(_maxPressure, pressure);
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
}
