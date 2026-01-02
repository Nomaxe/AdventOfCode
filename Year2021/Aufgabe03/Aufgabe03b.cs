using AdventOfCode.Utils;
using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2021;

internal class Aufgabe03b : IAufgabe
{
    private readonly List<bool[]> _input;

    public Aufgabe03b()
    {
        var input = Utilities.ReadInput(2021, 3);
        _input = new(input.Length);
        for (int i = 0; i < input.Length; i++)
        {
            _input.Add(new bool[input[i].Length]);
            for (int j = 0; j < input[i].Length; j++)
            {
                _input[i][j] = input[i][j] == '1';
            }
        }
    }

    public string Calc()
    {
        return (GetValue(_input, 0, true).GetDecimalNumber() * GetValue(_input, 0, false).GetDecimalNumber()).ToString();
    }

    public static bool[] GetValue(List<bool[]> input, int index, bool use)
    {
        LargeCounter<bool> counter = new(2);

        foreach (var item in input)
        {
            counter.Add(item[index]);
        }

        bool max;
        if (counter.Count == 1)
        {
            max = counter.First().Key;
        }
        else
        {
            var countTrue = counter[true];
            var countFalse = counter[false];
            if (countTrue > countFalse)
            {
                max = !(true ^ use);
            }
            else if (countFalse > countTrue)
            {
                max = !(false ^ use);
            }
            else
            {
                max = use;
            }
        }

        List<bool[]> nextList = new(input.Count);
        foreach (var item in input.Where(x => x[index] == max))
        {
            nextList.Add(item);
        }

        if (nextList.Count == 1)
        {
            return nextList[0];
        }

        return GetValue(nextList, index + 1, use);
    }
}
