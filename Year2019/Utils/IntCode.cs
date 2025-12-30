using AdventOfCode.Utils;

namespace AdventOfCode.Year2019.Utils;

internal class IntCode
{
    public const int NewLineNumber = 10;

    public IReadOnlyList<long> Codes => _code.AsReadOnly();
    public IReadOnlyList<long> Out => _out.AsReadOnly();
    public bool DidHalt => _code[_pointer] % 100 == 99;

    public bool WaitOnInput { get; init; } = false;
    private readonly long[] _code;
    private readonly Dictionary<long, long> _memory = [];
    private long _pointer = 0;
    private long _relativeBase = 0;
    private readonly Queue<long> _input = [];
    private readonly List<long> _out = [];

    private readonly long[] _orginal;

    public IntCode(int year, int day) : this(Utilities.ReadInput(year, day))
    {

    }

    public IntCode(string[] input)
    {
        var split = input[0].Split(',');
        _code = new long[split.Length];
        for (int i = 0; i < split.Length; i++)
        {
            _code[i] = long.Parse(split[i]);
        }

        _orginal = new long[split.Length];
        Array.Copy(_code, _orginal, _code.Length);
    }

    public void Calc()
    {
        do
        {
            switch (_code[_pointer] % 100)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Multiply();
                    break;
                case 3:
                    if (WaitOnInput && _input.Count == 0)
                    {
                        return;
                    }
                    Input();
                    break;
                case 4:
                    Output();
                    break;
                case 5:
                    JumpIfTrue();
                    break;
                case 6:
                    JumpIfFalse();
                    break;
                case 7:
                    LessThan();
                    break;
                case 8:
                    Equals();
                    break;
                case 9:
                    AdjustRelativeBase();
                    break;
                case 99:
                    return;
                default:
                    throw new NotImplementedException();
            }
        } while (_pointer < _code.Length);
    }

    public void AddInput(long input)
    {
        _input.Enqueue(input);
    }

    public void AddInput(char input)
    {
        _input.Enqueue(input);
    }

    public void AddInputNewLine()
    {
        _input.Enqueue(NewLineNumber);
    }

    public void AddInput(string input)
    {
        foreach (var character in input)
        {
            AddInput(character);
        }

        AddInputNewLine();
    }

    public bool HasInput => _input.Count > 0;

    public void SetCode(int position, int value)
    {
        _code[position] = value;
    }

    public void ClearOut()
    {
        _out.Clear();
    }

    public void Reset()
    {
        _pointer = 0;
        _relativeBase = 0;
        _memory.Clear();
        _input.Clear();
        _out.Clear();
        Array.Copy(_orginal, _code, _orginal.Length);
    }

    public void Draw()
    {
        foreach (var item in _out)
        {
            if (item == NewLineNumber)
            {
                Console.WriteLine();
                continue;
            }

            Console.Write((char)item);
        }
    }

    private void Add()
    {
        SetValue(GetIndex(3), GetValue(1) + GetValue(2));
        _pointer += 4;
    }

    private void Multiply()
    {
        SetValue(GetIndex(3), GetValue(1) * GetValue(2));
        _pointer += 4;
    }

    private void Input()
    {
        SetValue(GetIndex(1), _input.Dequeue());
        _pointer += 2;
    }

    private void Output()
    {
        _out.Add(GetValue(1));
        _pointer += 2;
    }

    private void JumpIfTrue()
    {
        if (GetValue(1) == 0)
        {
            _pointer += 3;
            return;
        }

        _pointer = GetValue(2);
    }

    private void JumpIfFalse()
    {
        if (GetValue(1) != 0)
        {
            _pointer += 3;
            return;
        }

        _pointer = GetValue(2);
    }

    private void LessThan()
    {
        var result = GetValue(1) < GetValue(2);
        SetValue(GetIndex(3), result ? 1 : 0);
        _pointer += 4;
    }

    private void Equals()
    {
        var result = GetValue(1) == GetValue(2);
        SetValue(GetIndex(3), result ? 1 : 0);
        _pointer += 4;
    }

    private void AdjustRelativeBase()
    {
        _relativeBase += GetValue(1);
        _pointer += 2;
    }

    private long GetValue(int parameter)
    {
        var index = GetIndex(parameter);
        if (index < _code.Length)
        {
            return _code[index];
        }
        else
        {
            if (_memory.TryGetValue(index, out var value))
            {
                return value;
            }

            return 0;
        }
    }

    private void SetValue(long index, long value)
    {
        if (index < _code.Length)
        {
            _code[index] = value;
        }
        else
        {
            _memory[index] = value;
        }
    }

    private long GetIndex(int parameter)
    {
        return GetMode(parameter) switch
        {
            0 => _code[_pointer + parameter],
            1 => _pointer + parameter,
            2 => _code[_pointer + parameter] + _relativeBase,
            _ => throw new NotImplementedException(),
        };
    }

    private long GetMode(int parameter)
    {
        return parameter switch
        {
            1 => _code[_pointer] % 1000 / 100,
            2 => _code[_pointer] % 10000 / 1000,
            3 => _code[_pointer] % 100000 / 10000,
            _ => throw new NotImplementedException(),
        };
    }
}
