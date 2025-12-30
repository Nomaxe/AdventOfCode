using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2023, 6);
    }

    public string Calc()
    {
        int count = 1;

        var time = _input[0].GetUnsignedNumbers();
        var distance = _input[1].GetUnsignedNumbers();

        for (int i = 0; i < time.Length; i++)
        {
            int raceCount = 0;

            for (int j = 1; j <= time[i]; j++)
            {
                var raceDistance = j * (time[i] - j);

                if (raceDistance > distance[i])
                {
                    raceCount++;
                }
            }

            count *= raceCount;
        }

        return count.ToString();
    }
}
