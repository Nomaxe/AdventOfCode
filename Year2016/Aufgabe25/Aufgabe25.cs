using AdventOfCode.Utils;
using AdventOfCode.Year2016.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe25 : IAufgabe
{
    private readonly AssembunnyCode _code;

    public Aufgabe25()
    {
        _code = new(2016, 25);
    }

    public string Calc()
    {
        int startValue = 0;

        while (true)
        {
            int expect = 0;

            _code.Reset();
            _code.SetRegister('a', startValue);
            for (int i = 0; i < 20; i++)
            {
                _code.Calc();
                if (_code.Out.Count == i + 1 && _code.Out[^1] == expect)
                {
                    if (i == 19)
                    {
                        return startValue.ToString();
                    }

                    expect = (expect + 1) % 2;
                }
                else
                {
                    break;
                }
            }

            startValue++;
        }
    }
}
