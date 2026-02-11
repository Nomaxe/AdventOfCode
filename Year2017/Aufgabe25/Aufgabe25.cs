using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2017;

internal class Aufgabe25 : IAufgabe
{
    private readonly string[] _input;
    private readonly int _steps;
    private readonly Dictionary<char, Instruction[]> _states;
    private readonly Dictionary<int, int> _tape;

    public Aufgabe25()
    {
        _input = Utilities.ReadInput(2017, 25);
        _steps = _input[1].GetNumber(36);
        _states = new(6);
        _tape = [];
    }

    public string Calc()
    {
        for (int i = 3; i < _input.Length; i += 10)
        {
            var state = _input[i][9];
            Instruction[] instructions = [new(_input[(i + 2)..(i + 5)]), new(_input[(i + 6)..(i + 9)])];
            _states.Add(state, instructions);
        }

        char currentState = 'A';
        int currentPosition = 0;

        for (int i = 0; i < _steps; i++)
        {
            ref var currentValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_tape, currentPosition, out _);
            var instruction = _states[currentState][currentValue];
            currentValue = instruction.Write;
            currentPosition += instruction.Direction;
            currentState = instruction.State;
        }

        return _tape.Values.Count(x => x == 1).ToString();
    }

    private readonly struct Instruction
    {
        public int Write { get; private init; }
        public int Direction { get; private init; }
        public char State { get; private init; }

        public Instruction(string[] input)
        {
            Write = input[0][^2].ToNumber();
            Direction = input[1][27] == 'r' ? 1 : -1;
            State = input[2][^2];
        }
    }
}
