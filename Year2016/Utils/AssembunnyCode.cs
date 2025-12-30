using AdventOfCode.Utils;

namespace AdventOfCode.Year2016.Utils;

internal class AssembunnyCode
{
    private readonly Instruction[] _code;
    private readonly int[] _registers;
    private int _currentPointer;
    private readonly List<int> _out;
    private readonly Instruction[] _orginalCode;

    public IReadOnlyList<int> Out => _out.AsReadOnly();

    public AssembunnyCode(int year, int day)
    {
        var input = Utilities.ReadInput(year, day);
        _code = new Instruction[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _code[i] = new(input[i]);
        }
        _orginalCode = new Instruction[input.Length];
        Array.Copy(_code, _orginalCode, _code.Length);
        _registers = new int[4];
        _currentPointer = 0;
        _out = new();
    }

    public void SetRegister(char register, int value)
    {
        _registers[register - 'a'] = value;
    }

    public int GetRegister(char register)
    {
        return _registers[register - 'a'];
    }

    public void Reset()
    {
        _currentPointer = 0;
        for (int i = 0; i < _registers.Length; i++)
        {
            _registers[0] = 0;
        }
        Array.Copy(_orginalCode, _code, _code.Length);
        _out.Clear();
    }

    public void Calc()
    {
        while (_currentPointer < _code.Length)
        {
            if (MulOptimisation())
            {
                continue;
            }

            var instruction = _code[_currentPointer];

            switch (instruction.Operator)
            {
                case Operator.Copy:
                    Copy(instruction);
                    break;
                case Operator.Increase:
                    Increase(instruction);
                    break;
                case Operator.Decrease:
                    Decrease(instruction);
                    break;
                case Operator.Jump:
                    Jump(instruction);
                    break;
                case Operator.Toggle:
                    Toggle(instruction);
                    break;
                case Operator.Out:
                    _currentPointer++;
                    _out.Add(GetValue(instruction.Argument1));
                    return;
            }
        }
    }

    private void Copy(Instruction instruction)
    {
        _currentPointer++;

        if (!char.IsAsciiLetterLower(instruction.Argument2![0]))
        {
            return;
        }

        var value = GetValue(instruction.Argument1);
        _registers[instruction.Argument2![0] - 'a'] = value;
    }

    private void Increase(Instruction instruction)
    {
        _registers[instruction.Argument1[0] - 'a']++;
        _currentPointer++;
    }

    private void Decrease(Instruction instruction)
    {
        _registers[instruction.Argument1[0] - 'a']--;
        _currentPointer++;
    }

    private void Jump(Instruction instruction)
    {
        var value = GetValue(instruction.Argument1);
        if (value != 0)
        {
            _currentPointer += GetValue(instruction.Argument2!);
            return;
        }

        _currentPointer++;
    }

    private void Toggle(Instruction instruction)
    {
        var index = _currentPointer + GetValue(instruction.Argument1);
        _currentPointer++;

        if (index < 0 || index >= _code.Length)
        {
            return;
        }

        ref var toogleInstruction = ref _code[index];
        toogleInstruction.Toggle();
    }

    private bool MulOptimisation()
    {
        if (_currentPointer + 5 >= _code.Length)
        {
            return false;
        }

        //Annahme das die Reihenfolge von ++ & -- immer gleich ist
        if (_code[_currentPointer].Operator == Operator.Copy &&
            _code[_currentPointer + 1].Operator == Operator.Increase &&
            _code[_currentPointer + 2].Operator == Operator.Decrease &&
            _code[_currentPointer + 3].Operator == Operator.Jump &&
            _code[_currentPointer + 3].Argument2 == "-2" &&
            _code[_currentPointer + 4].Operator == Operator.Decrease &&
            _code[_currentPointer + 5].Operator == Operator.Jump &&
            _code[_currentPointer + 5].Argument2 == "-5")
        {
            var resultCharacter = _code[_currentPointer + 1].Argument1[0];
            var startValue = GetValue(_code[_currentPointer].Argument1);
            var innerLoopCharacter = _code[_currentPointer + 3].Argument1[0];
            var outerLoopCharacter = _code[_currentPointer + 5].Argument1[0];

            SetRegister(innerLoopCharacter, startValue);
            SetRegister(resultCharacter, GetRegister(resultCharacter) + GetRegister(innerLoopCharacter) * GetRegister(outerLoopCharacter));
            SetRegister(innerLoopCharacter, 0);
            SetRegister(outerLoopCharacter, 0);
            _currentPointer += 6;
            return true;
        }

        return false;
    }

    private int GetValue(string value)
    {
        return value switch
        {
            "a" => _registers[0],
            "b" => _registers[1],
            "c" => _registers[2],
            "d" => _registers[3],
            _ => int.Parse(value),
        };
    }

    private struct Instruction
    {
        public Operator Operator { get; private set; }
        public readonly string Argument1 { get; private init; }
        public readonly string? Argument2 { get; private init; }

        public Instruction(string line)
        {
            var split = line.Split(' ');
            Operator = split[0] switch
            {
                "cpy" => Operator.Copy,
                "inc" => Operator.Increase,
                "dec" => Operator.Decrease,
                "jnz" => Operator.Jump,
                "tgl" => Operator.Toggle,
                "out" => Operator.Out,
                _ => throw new NotImplementedException()
            };
            Argument1 = split[1];
            if (split.Length > 2)
            {
                Argument2 = split[2];
            }
        }

        public void Toggle()
        {
            Operator = Operator switch
            {
                Operator.Increase => Operator.Decrease,
                Operator.Decrease or Operator.Toggle => Operator.Increase,
                Operator.Jump => Operator.Copy,
                Operator.Copy => Operator.Jump,
                _ => throw new NotImplementedException()
            };
        }
    }

    private enum Operator
    {
        Copy,
        Increase,
        Decrease,
        Jump,
        Toggle,
        Out
    }
}
