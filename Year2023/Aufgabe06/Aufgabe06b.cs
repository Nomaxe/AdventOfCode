using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2023, 6);
    }

    public string Calc()
    {
        var time = long.Parse(string.Join(string.Empty, _input[0].GetUnsignedNumbers()));
        var distance = long.Parse(string.Join(string.Empty, _input[1].GetUnsignedNumbers()));

        int count = 0;
        for (long i = 1; i <= time; i++)
        {
            var raceDistance = i * (time - i);

            if (raceDistance > distance)
            {
                count++;
            }
        }

        return count.ToString();
    }
}
