using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe25 : IAufgabe
{
    private readonly IntCode _intCode;
    public Aufgabe25()
    {
        _intCode = new(2019, 25)
        {
            WaitOnInput = true
        };
    }

    public string Calc()
    {
        _intCode.AddInput("north");
        _intCode.AddInput("east");
        _intCode.AddInput("north");
        _intCode.AddInput("east");
        _intCode.AddInput("take semiconductor");
        _intCode.AddInput("west");
        _intCode.AddInput("south");
        _intCode.AddInput("west");
        _intCode.AddInput("south");

        _intCode.AddInput("east");
        _intCode.AddInput("north");
        _intCode.AddInput("take coin");
        _intCode.AddInput("south");
        _intCode.AddInput("east");
        _intCode.AddInput("take candy cane");
        _intCode.AddInput("west");
        _intCode.AddInput("west");

        _intCode.AddInput("south");
        _intCode.AddInput("east");
        _intCode.AddInput("take mouse");
        _intCode.AddInput("south");
        _intCode.AddInput("west");

        _intCode.Calc();

        int index = _intCode.Out.Count - 46;
        long result = 0;

        do
        {
            result *= 10;
            result += _intCode.Out[index] - '0';
            index++;
        } while (_intCode.Out[index] != 32);

        return result.ToString();
    }
}
