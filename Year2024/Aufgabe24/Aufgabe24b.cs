using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe24b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, bool> _wires = [];
    private List<Gate> _gates = [];
    private readonly List<Gate> _allGates = [];
    private readonly List<string> _swappedGates = [];

    public Aufgabe24b()
    {
        _input = Utilities.ReadInput(2024, 24);
    }

    public string Calc()
    {
        bool whiteline = false;

        foreach (var line in _input)
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

        _allGates.AddRange(_gates);

        List<Gate> rule1 = [];
        List<Gate> rule2 = [];

        foreach (var gate in _gates)
        {
            if (gate.Result.StartsWith('z') && gate.Connection != Connection.XOR && gate.Result != "z45")
            {
                rule1.Add(gate);
                _swappedGates.Add(gate.Result);
            }

            if (!gate.Left.StartsWith('x') && !gate.Left.StartsWith('y') &&
                !gate.Right.StartsWith('x') && !gate.Right.StartsWith('y') &&
                !gate.Result.StartsWith('z') &&
                gate.Connection == Connection.XOR)
            {
                rule2.Add(gate);
                _swappedGates.Add(gate.Result);
            }
        }

        foreach (var gate in rule2)
        {
            var otherGate = rule1.Find(x => x.Result == 'z' + (int.Parse(FindGate(gate.Result)[1..]) - 1).ToString())!;
            (gate.Result, otherGate.Result) = (otherGate.Result, gate.Result);
        }

        foreach (var wire in _wires.Keys.Where(x => x.StartsWith('x') || x.StartsWith('y')))
        {
            _wires[wire] = true;
        }

        CalcConfiguration();
        var x = GetDecimalNumber('x');
        var y = GetDecimalNumber('y');
        var z = GetDecimalNumber('z');
        var failedBit = ((z ^ (x + y)).ToString("b").Length - 1).ToString("D2");
        var failedGates = _allGates.Where(x => x.Right[1..] == failedBit || x.Left[1..] == failedBit).ToList();
        _swappedGates.Add(failedGates[0].Result);
        _swappedGates.Add(failedGates[1].Result);
        (failedGates[0].Result, failedGates[1].Result) = (failedGates[1].Result, failedGates[0].Result);

        return string.Join(',', _swappedGates.Order());
    }

    private void CalcConfiguration()
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
    }

    private string FindGate(string result)
    {
        var gate = _gates.FindAll(x => x.Left == result || x.Right == result);
        var zGate = gate.Find(x => x.Result.StartsWith('z'));
        if (zGate != null)
        {
            return zGate.Result;
        }

        return FindGate(gate.First().Result);
    }

    private ulong GetDecimalNumber(char c)
    {
        return _wires.Where(x => x.Key.StartsWith(c)).OrderByDescending(x => x.Key).Select(x => x.Value).ToList().GetDecimalNumber();
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

    private class Gate
    {
        public string Left { get; private init; }
        public string Right { get; private init; }
        public Connection Connection { get; private init; }
        public string Result { get; set; }

        public Gate(string left, string right, Connection connection, string result)
        {
            Left = left;
            Right = right;
            Connection = connection;
            Result = result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Gate gate &&
                   Left == gate.Left &&
                   Right == gate.Right &&
                   Connection == gate.Connection &&
                   Result == gate.Result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Connection, Result);
        }

        public override string ToString()
        {
            return $"{Left} {Connection} {Right} -> {Result}";
        }
    }

    private enum Connection
    {
        AND,
        OR,
        XOR
    }
}
