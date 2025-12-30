using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe01b : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInputAsIntArray(2018, 1);
    }

    public string Calc()
    {
        int index = 0;
        int currentFrequency = 0;
        HashSet<int> frequencies = [];

        while (true)
        {
            currentFrequency += _input[index];

            if (frequencies.Contains(currentFrequency))
            {
                return currentFrequency.ToString();
            }

            frequencies.Add(currentFrequency);
            index++;
            if (index == _input.Length)
            {
                index = 0;
            }
        }
    }
}
