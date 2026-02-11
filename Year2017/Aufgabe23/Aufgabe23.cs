using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe23 : IAufgabe
{
    private readonly string[] _input;
    private long _registerA = 0;
    private long _registerB = 0;
    private long _registerC = 0;
    private long _registerD = 0;
    private long _registerE = 0;
    private long _registerF = 0;
    private long _registerG = 0;
    private long _registerH = 0;
    private readonly List<Instruction> _instructions;
    private int _pointer = 0;
    private int _result = 0;

    public Aufgabe23()
    {
        _input = Utilities.ReadInput(2017, 23);
        _instructions = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            _instructions.Add(new(split[0], split[1][0], split[2]));
        }

        while (_pointer < _instructions.Count)
        {
            switch (_instructions[_pointer].Mode)
            {
                case "set":
                    Set(_instructions[_pointer]);
                    break;
                case "sub":
                    Sub(_instructions[_pointer]);
                    break;
                case "mul":
                    Mul(_instructions[_pointer]);
                    break;
                case "jnz":
                    Jnz(_instructions[_pointer]);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return _result.ToString();
    }

    private void Set(Instruction instruction)
    {
        SetValue(instruction.Value1, GetValue(instruction.Value2));
        _pointer++;
    }

    private void Sub(Instruction instruction)
    {
        SetValue(instruction.Value1, GetValue(instruction.Value1.ToString()) - GetValue(instruction.Value2));
        _pointer++;
    }

    private void Mul(Instruction instruction)
    {
        SetValue(instruction.Value1, GetValue(instruction.Value1.ToString()) * GetValue(instruction.Value2));
        _pointer++;
        _result++;
    }

    private void Jnz(Instruction instruction)
    {
        var value = GetValue(instruction.Value1.ToString());
        if (value != 0)
        {
            _pointer += int.Parse(instruction.Value2);
            return;
        }

        _pointer++;
    }

    private void SetValue(char register, long value)
    {
        switch (register)
        {
            case 'a':
                _registerA = value;
                break;
            case 'b':
                _registerB = value;
                break;
            case 'c':
                _registerC = value;
                break;
            case 'd':
                _registerD = value;
                break;
            case 'e':
                _registerE = value;
                break;
            case 'f':
                _registerF = value;
                break;
            case 'g':
                _registerG = value;
                break;
            case 'h':
                _registerH = value;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private long GetValue(string value)
    {
        return value switch
        {
            "a" => _registerA,
            "b" => _registerB,
            "c" => _registerC,
            "d" => _registerD,
            "e" => _registerE,
            "f" => _registerF,
            "g" => _registerG,
            "h" => _registerH,
            _ => long.Parse(value),
        };
    }

    private record struct Instruction(string Mode, char Value1, string Value2)
    {

    }
}
