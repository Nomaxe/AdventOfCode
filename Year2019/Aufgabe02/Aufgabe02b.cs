using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe02b : IAufgabe
{
    private readonly IntCode _intCode;

    public Aufgabe02b()
    {
        _intCode = new(2019, 2);
    }

    public string Calc()
    {
        for (int i = 0; i <= 99; i++)
        {
            for (int j = 0; j <= 99; j++)
            {
                _intCode.Reset();
                _intCode.SetCode(1, i);
                _intCode.SetCode(2, j);
                _intCode.Calc();
                if (_intCode.Codes[0] == 19690720)
                {
                    return (100 * i + j).ToString();
                }
            }
        }

        throw new NotImplementedException();
    }
}
