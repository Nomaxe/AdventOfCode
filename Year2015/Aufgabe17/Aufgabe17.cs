using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe17 : IAufgabe
{
    private readonly int[] _input;
    private int _count = 0;

    public Aufgabe17()
    {
        _input = Utilities.ReadInputAsIntArray(2015, 17);
    }

    public string Calc()
    {
        Check(150, 0);

        return _count.ToString();
    }

    private void Check(int remaining, int index)
    {
        if (index == _input.Length)
        {
            if (remaining == 0)
            {
                _count++;
            }
            return;
        }

        Check(remaining, index + 1);
        Check(remaining - _input[index], index + 1);
    }
}
