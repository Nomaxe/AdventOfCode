using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe17 : IAufgabe
{
    private readonly string[] _input;
    private ulong _registerA;
    private ulong _registerB;
    private ulong _registerC;
    private readonly List<int> _instructions = [];
    private int _pointer = 0;
    private readonly List<int> _out = [];

    public Aufgabe17()
    {
        _input = Utilities.ReadInput(2024, 17);
    }

    public string Calc()
    {
        _registerA = ulong.Parse(_input[0][12..]);
        _registerB = ulong.Parse(_input[1][12..]);
        _registerC = ulong.Parse(_input[2][12..]);
        foreach (var instruction in _input[^1][9..].Split(','))
        {
            _instructions.Add(int.Parse(instruction));
        }

        while (_pointer < _instructions.Count)
        {
            var instruction = _instructions[_pointer];
            var value = _instructions[_pointer + 1];

            switch (instruction)
            {
                case 0:
                    Instruction0(value);
                    break;
                case 1:
                    Instruction1(value);
                    break;
                case 2:
                    Instruction2(value);
                    break;
                case 3:
                    Instruction3(value);
                    break;
                case 4:
                    Instruction4(value);
                    break;
                case 5:
                    Instruction5(value);
                    break;
                case 6:
                    Instruction6(value);
                    break;
                case 7:
                    Instruction7(value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return string.Join(',', _out);
    }

    private void Instruction0(int value)
    {
        _registerA /= (ulong)Math.Pow(2, GetComboValue(value));
        _pointer += 2;
    }

    private void Instruction1(int value)
    {
        _registerB ^= (ulong)value;
        _pointer += 2;
    }

    private void Instruction2(int value)
    {
        _registerB = GetComboValue(value) % 8;
        _pointer += 2;
    }

    private void Instruction3(int value)
    {
        if (_registerA == 0)
        {
            _pointer += 2;
            return;
        }

        _pointer = value;
    }

    private void Instruction4(int value)
    {
        _registerB ^= _registerC;
        _pointer += 2;
    }

    private void Instruction5(int value)
    {
        _out.Add((int)(GetComboValue(value) % 8));
        _pointer += 2;
    }

    private void Instruction6(int value)
    {
        _registerB = _registerA / Convert.ToUInt64(Math.Pow(2, GetComboValue(value)));
        _pointer += 2;
    }

    private void Instruction7(int value)
    {
        _registerC = _registerA / Convert.ToUInt64(Math.Pow(2, GetComboValue(value)));
        _pointer += 2;
    }

    private ulong GetComboValue(int value)
    {
        return value switch
        {
            0 or 1 or 2 or 3 => (ulong)value,
            4 => _registerA,
            5 => _registerB,
            6 => _registerC,
            _ => throw new NotImplementedException()
        };
    }
}
