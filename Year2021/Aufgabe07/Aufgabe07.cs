using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe07 : IAufgabe
{
    private readonly List<int> _input;

    public Aufgabe07()
    {
        _input = Utilities.ReadInputAsIntList(2021, 7);
    }

    public string Calc()
    {
        var min = _input.Min();
        var max = _input.Max();
        var minSum = int.MaxValue;

        for (int i = min; i <= max; i++)
        {
            var sum = _input.Sum(x => int.Abs(x - i));

            if (sum < minSum)
            {
                minSum = sum;
            }
            else
            {
                return minSum.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
