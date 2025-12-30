using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe24 : IAufgabe
{
    private readonly Dictionary<string, bool> _wires = [];
    private List<Gate> _gates = [];

    public Aufgabe24()
    {
        var input = Utilities.ReadInput(2024, 24);
        bool whiteline = false;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                whiteline = true;
                continue;
            }

            if (!whiteline)
            {
                var split = line.Split(' ');
                _wires.Add(split[0][..3], split[1] == "1");
            }
            else
            {
                var split = line.Split(' ');
                _gates.Add(new(split[0], split[2], Enum.Parse<Connection>(split[1]), split[^1]));
            }
        }
    }

    public string Calc()
    {
        while (_gates.Count > 0)
        {
            List<Gate> nextGates = [];

            foreach (var gate in _gates)
            {
                if (!_wires.TryGetValue(gate.Left, out bool left))
                {
                    nextGates.Add(gate);
                    continue;
                }

                if (!_wires.TryGetValue(gate.Right, out bool right))
                {
                    nextGates.Add(gate);
                    continue;
                }

                _wires.Add(gate.Result, CalcGate(left, right, gate.Connection));
            }

            _gates = nextGates;
        }

        var result = _wires.Where(x => x.Key.StartsWith('z')).OrderByDescending(x => x.Key).ToList();
        ulong value = 1;
        ulong resultNumber = 0;
        for (int i = result.Count - 1; i >= 0; i--)
        {
            if (result[i].Value)
            {
                resultNumber += value;
            }

            value *= 2;
        }

        return resultNumber.ToString();
    }

    private static bool CalcGate(bool left, bool right, Connection connection)
    {
        return connection switch
        {
            Connection.AND => left && right,
            Connection.OR => left || right,
            Connection.XOR => left ^ right,
            _ => throw new NotImplementedException(),
        };
    }

    private readonly struct Gate
    {
        public string Left { get; private init; }
        public string Right { get; private init; }
        public Connection Connection { get; private init; }
        public string Result { get; private init; }

        public Gate(string left, string right, Connection connection, string result)
        {
            Left = left;
            Right = right;
            Connection = connection;
            Result = result;
        }
    }

    private enum Connection
    {
        AND,
        OR,
        XOR
    }
}
