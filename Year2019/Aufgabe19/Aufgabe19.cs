using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe19 : IAufgabe
{
    private readonly IntCode _intcode;

    public Aufgabe19()
    {
        _intcode = new(2019, 19);
    }

    public string Calc()
    {
        int result = 0;

        for (int y = 0; y < 50; y++)
        {
            for (int x = 0; x < 50; x++)
            {
                _intcode.AddInput(x);
                _intcode.AddInput(y);
                _intcode.Calc();
                if (_intcode.Out[0] == 1)
                {
                    result++;
                }
                _intcode.Reset();
            }
        }

        return result.ToString();
    }
}
