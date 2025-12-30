using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<int, int> _pipes;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2017, 12);
        _pipes = new(_input.Length);
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
        }

        Queue<int> queue = new();
        queue.Enqueue(0);
        HashSet<int> visited = [];

        while (queue.Count > 0)
        {
            int nextProgram = queue.Dequeue();
            if (!visited.Add(nextProgram))
            {
                continue;
            }

            foreach (var program in _pipes[nextProgram].Where(x => !visited.Contains(x)))
            {
                queue.Enqueue(program);
            }
        }

        return visited.Count.ToString();
    }
}
