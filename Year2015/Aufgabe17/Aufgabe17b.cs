using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe17b : IAufgabe
{
    private readonly int[] _input;
    private readonly DictionaryCounter<int> _counter;

    public Aufgabe17b()
    {
        _input = Utilities.ReadInputAsArray<int>(2015, 17);
        _counter = new();
    }

    public string Calc()
    {
        Check(150, 0, 0);

        return _counter[_counter.Keys.Min()].ToString();
    }

    private void Check(int remaining, int index, int count)
    {
        if (index == _input.Length)
        {
            if (remaining == 0)
            {
                _counter.Add(count);
            }
            return;
        }

        Check(remaining, index + 1, count);
        Check(remaining - _input[index], index + 1, count + 1);
    }
}
