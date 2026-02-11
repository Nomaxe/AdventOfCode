using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016;

internal partial class Aufgabe11 : IAufgabe
{
    private readonly string[] _input;
    private readonly int[] _floorCount;

    public Aufgabe11()
    {
        _input = Utilities.ReadInput(2016, 11);
        _floorCount = new int[_input.Length];
    }

    public string Calc()
    {
        for (int i = 0; i < _floorCount.Length; i++)
        {
            _floorCount[i] = ObjectCount().Count(_input[i]);
        }

        int count = 0;

        for (int i = 0; i < _floorCount.Length - 1; i++)
        {
            count += 2 * (_floorCount[i] - 1) - 1;
            _floorCount[i + 1] += _floorCount[i];
        }

        return count.ToString();
    }

    [GeneratedRegex("(generator|microchip)")]
    private static partial Regex ObjectCount();
}
