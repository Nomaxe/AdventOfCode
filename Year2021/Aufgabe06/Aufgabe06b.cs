using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe06b : IAufgabe
{
    private LargeCounter<int> _counter = [];

    public Aufgabe06b()
    {
        var input = Utilities.ReadInput(2021, 6);
        var split = input[0].Split(',');
        foreach (var item in split)
        {
            _counter.Add(int.Parse(item));
        }
    }

    public string Calc()
    {
        for (int i = 0; i < 256; i++)
        {
            LargeCounter<int> nextCounter = [];

            foreach (var item in _counter)
            {
                if (item.Key == 0)
                {
                    nextCounter.Add(6, item.Value);
                    nextCounter.Add(8, item.Value);
                }
                else
                {
                    nextCounter.Add(item.Key - 1, item.Value);
                }
            }

            _counter = nextCounter;
        }

        return _counter.GetTotalCount().ToString();
    }
}
