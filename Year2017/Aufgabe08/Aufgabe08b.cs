using AdventOfCode.Utils;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2017;

internal class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, int> _register;
    private int _max = 0;

    public Aufgabe08b()
    {
        _input = Utilities.ReadInput(2017, 8);
        _register = [];
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            if (Check(GetValue(split[4]), split[5], int.Parse(split[6])))
            {
                SetRegister(split[0], split[1], int.Parse(split[2]));
            }
        }

        return _max.ToString();
    }

    private int GetValue(string register)
    {
        if (_register.TryGetValue(register, out var value))
        {
            return value;
        }

        return 0;
    }

    private static bool Check(int registerValue, string op, int checkValue)
    {
        return op switch
        {
            ">" => registerValue > checkValue,
            ">=" => registerValue >= checkValue,
            "<" => registerValue < checkValue,
            "<=" => registerValue <= checkValue,
            "==" => registerValue == checkValue,
            "!=" => registerValue != checkValue,
            _ => throw new NotImplementedException()
        };
    }

    private void SetRegister(string register, string op, int value)
    {
        ref var currentValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_register, register, out _);

        switch (op)
        {
            case "inc":
                currentValue += value;
                break;
            case "dec":
                currentValue -= value;
                break;
            default:
                throw new NotImplementedException();
        }
        ;

        _max = int.Max(_max, currentValue);
    }
}
