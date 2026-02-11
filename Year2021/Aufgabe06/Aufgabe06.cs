using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe06 : IAufgabe
{
    private readonly List<int> _input;
    private LargeCounter<int> _counter = [];

    public Aufgabe06()
    {
        _input = Utilities.ReadInputAsList<int>(2021, 6, ',');
    }

    public string Calc()
    {
        foreach (var item in _input)
        {
            _counter.Add(item);
        }

        for (int i = 0; i < 80; i++)
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
