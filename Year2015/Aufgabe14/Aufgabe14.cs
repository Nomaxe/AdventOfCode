using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private const int Seconds = 2503;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2015, 14);
    }

    public string Calc()
    {
        int maxDistance = 0;

        foreach (var line in _input)
        {
            int distance = 0;
            var numbers = line.GetUnsignedNumbers();
            var times = Seconds / (numbers[1] + numbers[2]);
            var rest = Seconds % (numbers[1] + numbers[2]);

            distance += times * numbers[0] * numbers[1];

            if (rest > 0)
            {
                rest = int.Min(rest, numbers[1]);
                distance += rest * numbers[0];
            }

            maxDistance = int.Max(distance, maxDistance);
        }

        return maxDistance.ToString();
    }
}
