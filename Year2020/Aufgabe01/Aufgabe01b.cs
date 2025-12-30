using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe01b : IAufgabe
{
    private readonly HashSet<int> _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInputAsIntHashSet(2020, 1);
    }

    public string Calc()
    {
        int skip = 1;

        foreach (var item in _input)
        {
            foreach (var item2 in _input.Skip(skip))
            {
                var other = 2020 - item - item2;
                if (other <= 0)
                {
                    continue;
                }

                if (_input.Contains(other))
                {
                    return (item * item2 * other).ToString();
                }
            }

            skip++;
        }

        throw new NotImplementedException();
    }
}
