using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2018, 2);
    }

    public string Calc()
    {
        int count3 = 0;
        int count2 = 0;

        foreach (var line in _input)
        {
            LargeCounter<char> counter = [];
            foreach (var character in line)
            {
                counter.Add(character);
            }

            if (counter.HasCount(3))
            {
                count3++;
            }
            if (counter.HasCount(2))
            {
                count2++;
            }
        }

        return (count3 * count2).ToString();
    }
}
