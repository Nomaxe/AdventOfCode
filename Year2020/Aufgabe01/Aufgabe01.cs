using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe01 : IAufgabe
{
    private readonly HashSet<int> _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInputAsIntHashSet(2020, 1);
    }

    public string Calc()
    {
        foreach (var item in _input)
        {
            var other = 2020 - item;

            if (_input.Contains(other))
            {
                return (item * other).ToString();
            }
        }

        throw new NotImplementedException();
    }
}
