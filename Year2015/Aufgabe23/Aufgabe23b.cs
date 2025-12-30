using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2015;

internal class Aufgabe23b : IAufgabe
{
    private readonly string[] _input;
    private int _currentIndex;
    private Dictionary<char, uint> _registers;

    public Aufgabe23b()
    {
        _input = Utilities.ReadInput(2015, 23);
        _currentIndex = 0;
        _registers = new(2)
        {
            { 'a', 1 },
            { 'b', 0 }
        };
    }

    public string Calc()
    {
        while (_currentIndex < _input.Length)
        {
            var instruction = _input[_currentIndex];

            switch (instruction[..3])
            {
                case "hlf":
                    Half(instruction[4]);
                    break;
                case "tpl":
                    Triple(instruction[4]);
                    break;
                case "inc":
                    Increment(instruction[4]);
                    break;
                case "jmp":
                    _currentIndex += int.Parse(instruction[4..]);
                    continue;
                case "jie":
                    JumpIsEven(instruction[4], instruction[7..]);
                    continue;
                case "jio":
                    JumpIsOne(instruction[4], instruction[7..]);
                    continue;

            }

            _currentIndex++;
        }

        return _registers['b'].ToString();
    }

    private void Half(char register)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(_registers, register);
        value /= 2;
    }

    private void Triple(char register)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(_registers, register);
        value *= 3;
    }

    private void Increment(char register)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(_registers, register);
        value++;
    }

    private void JumpIsEven(char register, string value)
    {
        if (_registers[register] % 2 == 0)
        {
            _currentIndex += int.Parse(value);
        }
        else
        {
            _currentIndex++;
        }
    }

    private void JumpIsOne(char register, string value)
    {
        if (_registers[register] == 1)
        {
            _currentIndex += int.Parse(value);
        }
        else
        {
            _currentIndex++;
        }
    }
}
