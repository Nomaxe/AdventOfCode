using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2017;

internal class Aufgabe18 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<char, long> _registers;

    public Aufgabe18()
    {
        _input = Utilities.ReadInput(2017, 18);
        _registers = [];
    }

    public string Calc()
    {
        long currentPosition = 0;
        long lastPlayedSound = 0;

        while (true)
        {
            var currentInstruction = _input[currentPosition];

            switch (currentInstruction[..3])
            {
                case "snd":
                    lastPlayedSound = GetRegisterValue(currentInstruction[4]);
                    break;
                case "set":
                    SetRegisterValue(currentInstruction[4], GetValue(currentInstruction[6..]));
                    break;
                case "add":
                    AddRegisterValue(currentInstruction[4], GetValue(currentInstruction[6..]));
                    break;
                case "mul":
                    MulRegisterValue(currentInstruction[4], GetValue(currentInstruction[6..]));
                    break;
                case "mod":
                    ModRegisterValue(currentInstruction[4], GetValue(currentInstruction[6..]));
                    break;
                case "rcv":
                    if (GetRegisterValue(currentInstruction[4]) != 0)
                    {
                        return lastPlayedSound.ToString();
                    }
                    break;
                case "jgz":
                    if (GetValue(currentInstruction[4].ToString()) > 0)
                    {
                        currentPosition += GetValue(currentInstruction[6..]);
                        continue;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            currentPosition++;
        }
    }

    private long GetValue(string value)
    {
        if (value.Length == 1 && char.IsLetter(value[0]))
        {
            return GetRegisterValue(value[0]);
        }

        return long.Parse(value);
    }

    private long GetRegisterValue(char register)
    {
        return _registers.GetValueOrDefault(register);
    }

    private void SetRegisterValue(char register, long value)
    {
        ref var registerValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_registers, register, out _);
        registerValue = value;
    }

    private void AddRegisterValue(char register, long value)
    {
        ref var registerValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_registers, register, out _);
        registerValue += value;
    }

    private void MulRegisterValue(char register, long value)
    {
        ref var registerValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_registers, register, out _);
        registerValue *= value;
    }

    private void ModRegisterValue(char register, long value)
    {
        ref var registerValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_registers, register, out _);
        registerValue %= value;
    }
}
