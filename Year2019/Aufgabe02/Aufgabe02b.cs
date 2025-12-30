using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe02b : IAufgabe
{
    public string Calc()
    {
        var intcode = new IntCode(2019, 2);
        for (int i = 0; i <= 99; i++)
        {
            for (int j = 0; j <= 99; j++)
            {
                intcode.Reset();
                intcode.SetCode(1, i);
                intcode.SetCode(2, j);
                intcode.Calc();
                if (intcode.Codes[0] == 19690720)
                {
                    return (100 * i + j).ToString();
                }
            }
        }

        throw new NotImplementedException();
    }
}
