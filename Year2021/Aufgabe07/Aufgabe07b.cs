using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe07b : IAufgabe
{
    private readonly List<int> _input;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInputAsList<int>(2021, 7, ',');
    }

    public string Calc()
    {
        var min = _input.Min();
        var max = _input.Max();
        var minSum = int.MaxValue;

        for (int i = min; i <= max; i++)
        {
            var sum = _input.Sum(x => GetFibonacci(int.Abs(x - i)));

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

    private static int GetFibonacci(int number)
    {
        int result = 0;

        for (int i = 1; i <= number; i++)
        {
            result += i;
        }

        return result;
    }
}
