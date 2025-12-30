using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe12b : IAufgabe
{
    private readonly string[] _input;
    private readonly int[] _registers;
    private int _currentPointer;

    public Aufgabe12b()
    {
        _input = Utilities.ReadInput(2016, 12);
        _registers = new int[4];
        _registers[2] = 1;
        _currentPointer = 0;
    }

    public string Calc()
    {
        while (_currentPointer < _input.Length)
        {
            var instruction = _input[_currentPointer];

            switch (instruction[..3])
            {
                case "cpy":
                    Copy(instruction);
                    break;
                case "inc":
                    Increase(instruction);
                    break;
                case "dec":
                    Decrease(instruction);
                    break;
                case "jnz":
                    Jump(instruction);
                    break;
            }
        }

        return _registers[0].ToString();
    }

    private void Copy(string instruction)
    {
        var split = instruction.Split(' ');
        var value = GetValue(split[1]);
        _registers[split[2][0] - 'a'] = value;
        _currentPointer++;
    }

    private void Increase(string instruction)
    {
        _registers[instruction[4] - 'a']++;
        _currentPointer++;
    }

    private void Decrease(string instruction)
    {
        _registers[instruction[4] - 'a']--;
        _currentPointer++;
    }

    private void Jump(string instruction)
    {
        var split = instruction.Split(' ');
        var value = GetValue(split[1]);
        if (value != 0)
        {
            _currentPointer += int.Parse(split[2]);
            return;
        }

        _currentPointer++;
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
}
