using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe17b : IAufgabe
{
    private readonly string _input;

    public Aufgabe17b()
    {
        _input = Utilities.ReadInput(2017, 17)[0];
    }

    public string Calc()
    {
        int times = int.Parse(_input);
        int valueAfter0 = 0;
        int currentPosition = 0;

        //0 ist immer vorne, somit ist nur die Position danach relevant
        for (int i = 1; i <= 50_000_000; i++)
        {
            currentPosition = (currentPosition + times) % i;

            if (currentPosition == 0)
            {
                valueAfter0 = i;
            }

            currentPosition++;
        }

        return valueAfter0.ToString();
    }
}
