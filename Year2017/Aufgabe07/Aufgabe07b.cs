using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _programs;
    private readonly LargeCounter<string> _programWeights;
    private readonly LargeCounter<string> _weights;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInput(2017, 7);
        _programs = new(_input.Length);
        _programWeights = new(_input.Length);
        _weights = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var whitespace = line.IndexOf(' ');
            var closeBracket = line.IndexOf(')');
            var program = line[..whitespace];
            var weight = ulong.Parse(line[(whitespace + 2)..closeBracket]);

            _weights.Add(program, weight);
            _programWeights.Add(program, weight);
            var subprogramsIndex = line.IndexOf("->");
            if (subprogramsIndex >= 0)
            {
                var subprograms = line[(subprogramsIndex + 3)..].Split(", ");
                _programs.Add(program, subprograms);
            }
            else
            {
                _programs.AddKey(program);
            }
        }

        var start = GetStart();
        FillWeights(start);
        HashSet<string> unbalancedPrograms = [];

        foreach (var programs in _programs.Where(x => x.Value.Count >= 3))
        {
            LargeCounter<ulong> counter = [];
            foreach (var subprograms in programs.Value)
            {
                counter.Add(_weights[subprograms]);
            }

            if (counter.Count == 2)
            {
                unbalancedPrograms.Add(programs.Key);
            }
        }

        List<string> checkList = [start];
        while (unbalancedPrograms.Count > 1)
        {
            List<string> nextCheckList = [];

            foreach (var program in checkList)
            {
                unbalancedPrograms.Remove(program);
                nextCheckList.AddRange(_programs[program]);
            }

            checkList = nextCheckList;
        }

        return GetResult(unbalancedPrograms.First()).ToString();
    }

    private string GetStart()
    {
        HashSet<string> start = new(_programs.Count);
        foreach (var key in _programs.Keys)
        {
            start.Add(key);
        }

        foreach (var program in _programs)
        {
            foreach (var subprogram in program.Value)
            {
                start.Remove(subprogram);
            }
        }

        return start.First();
    }

    private ulong FillWeights(string start)
    {
        ulong weight = 0;
        var subprograms = _programs[start];

        foreach (var subprogram in subprograms)
        {
            weight += FillWeights(subprogram);
        }

        _weights.Add(start, weight);
        return _weights[start];
    }

    private ulong GetResult(string program)
    {
        LargeCounter<ulong> counter = [];
        var programs = _programs[program];
        foreach (var subprograms in programs)
        {
            counter.Add(_weights[subprograms]);
        }

        var min = counter.GetMinKey();
        var max = counter.GetMaxKey();
        var programWeight = _programWeights[programs.First(x => _weights[x] == min)];
        return programWeight + (max - min);
    }
}
