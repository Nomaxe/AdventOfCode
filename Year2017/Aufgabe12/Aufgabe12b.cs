using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe12b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<int, int> _pipes;
    private readonly HashSet<int> _allPrograms;

    public Aufgabe12b()
    {
        _input = Utilities.ReadInput(2017, 12);
        _pipes = new(_input.Length);
        _allPrograms = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var firstSpaceIndex = line.IndexOf(' ');
            var id = int.Parse(line[..firstSpaceIndex]);
            var connections = line[(firstSpaceIndex + 5)..].Split(", ").Select(int.Parse).ToList();
            _pipes.Add(id, connections);
            foreach (var connection in connections)
            {
                _pipes.Add(connection, id);
            }
            _allPrograms.Add(id);
        }

        int groupCount = 0;
        while (_allPrograms.Count > 0)
        {
            CalcGroup(_allPrograms.First());
            groupCount++;
        }

        return groupCount.ToString();
    }

    private void CalcGroup(int program)
    {
        Queue<int> queue = new();
        queue.Enqueue(program);
        HashSet<int> visited = [];

        while (queue.Count > 0)
        {
            int nextProgram = queue.Dequeue();
            if (!visited.Add(nextProgram))
            {
                continue;
            }
            _allPrograms.Remove(nextProgram);

            foreach (var connectedProgram in _pipes[nextProgram].Where(x => !visited.Contains(x)))
            {
                queue.Enqueue(connectedProgram);
            }
        }
    }
}
