using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2017;

internal class Aufgabe18b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe18b()
    {
        _input = Utilities.ReadInput(2017, 18);
    }

    public string Calc()
    {
        Program one = new(_input, null, 0);
        Program two = new(_input, one, 1);
        one.AddOther(two);
        Program[] programs = [one, two];

        int index = -1;
        while (!one.IsWaiting || !two.IsWaiting)
        {
            index = (index + 1) % programs.Length;

            programs[index].Calc();
        }

        return two.SendPackages.ToString();
    }

    private class Program
    {
        public bool IsWaiting => _isWaiting;
        public int SendPackages { get; private set; }

        private readonly string[] _input;
        private readonly Dictionary<char, long> _registers;
        private long _currentPosition;
        private bool _isWaiting;
        private Program? _other;
        private readonly Queue<long> _queue;

        public Program(string[] input, Program? other, long programId)
        {
            _input = input;
            _registers = [];
            _other = other;
            _currentPosition = 0;
            _isWaiting = false;
            _queue = new();
            SetRegisterValue('p', programId);
        }

        public void AddOther(Program program)
        {
            _other = program;
        }

        public void Calc()
        {
            while (true)
            {
                var currentInstruction = _input[_currentPosition];

                switch (currentInstruction[..3])
                {
                    case "snd":
                        _other!.ReceiveValue(GetRegisterValue(currentInstruction[4]));
                        SendPackages++;
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
                        if (_queue.Count == 0)
                        {
                            _isWaiting = true;
                            return;
                        }

                        SetRegisterValue(currentInstruction[4], _queue.Dequeue());
                        break;
                    case "jgz":
                        if (GetValue(currentInstruction[4].ToString()) > 0)
                        {
                            _currentPosition += GetValue(currentInstruction[6..]);
                            continue;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

                _currentPosition++;
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

        private void ReceiveValue(long value)
        {
            _queue.Enqueue(value);
            _isWaiting = false;
        }
    }
}
