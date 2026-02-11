using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe17b : IAufgabe
{
    private readonly string[] _input;
    private ulong _registerA = 0;
    private ulong _registerB = 0;
    private ulong _registerC = 0;
    private readonly List<int> _instructions = [];
    private int _pointer = 0;
    private readonly List<int> _out = [];

    public Aufgabe17b()
    {
        _input = Utilities.ReadInput(2024, 17);
    }

    public string Calc()
    {
        foreach (var instruction in _input[^1][9..].Split(','))
        {
            _instructions.Add(int.Parse(instruction));
        }

        List<ulong> test = [0, 1, 2, 3, 4, 5, 6, 7];
        List<ulong> nextTest = [];
        int checkAmount = 1;
        ulong? result = null;

        do
        {
            foreach (var value in test)
            {
                Test(value);

                if (CheckResult(checkAmount))
                {
                    if (_instructions.SequenceEqual(_out))
                    {
                        result = value;
                        break;
                    }

                    var number = value * 8;
                    for (ulong i = 0; i < 8; i++)
                    {
                        nextTest.Add(number + i);
                    }
                }
            }

            test.Clear();
            test.AddRange(nextTest);
            checkAmount++;
            nextTest.Clear();
        } while (!result.HasValue);

        return result.Value.ToString();
    }

    private void Test(ulong registerA)
    {
        _registerA = registerA;
        _registerB = 0;
        _registerC = 0;
        _pointer = 0;
        _out.Clear();

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

    private bool CheckResult(int checkAmount)
    {
        for (int i = 1; i <= checkAmount; i++)
        {
            if (_instructions[^i] != _out[^i])
            {
                return false;
            }
        }

        return true;
    }
}
