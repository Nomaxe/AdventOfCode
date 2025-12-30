using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe23b : IAufgabe
{
    private readonly long _input;

    public Aufgabe23b()
    {
        _input = Utilities.ReadInput(2017, 23)[0].GetNumbers()[0] * 100 + 100000;
    }

    public string Calc()
    {
        int result = 0;

        for (long i = _input; i <= _input + 17000; i += 17)
        {
            var divider = 2;

            while (i % divider != 0)
            {
                divider++;
            }

            if (i != divider)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
