using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe10 : IAufgabe
{
    private readonly int[] _input;

    public Aufgabe10()
    {
        _input = Utilities.ReadInputAsArray<int>(2020, 10);
    }

    public string Calc()
    {
        Array.Sort(_input);

        int oneDifference = 0;
        int threeDifference = 1;
        int lastValue = 0;

        foreach (var number in _input)
        {
            var difference = number - lastValue;
            if (difference == 1)
            {
                oneDifference++;
            }
            else
            {
                threeDifference++;  //es gibt nur 1- und 3-Unterschiede
            }
            lastValue = number;
        }

        return (oneDifference * threeDifference).ToString();
    }
}
