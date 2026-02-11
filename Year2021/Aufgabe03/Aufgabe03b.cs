using AdventOfCode.Utils;
using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2021;

internal class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<bool[]> _list;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2021, 3);
        _list = new(_input.Length);
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            _list.Add(new bool[_input[i].Length]);
            for (int j = 0; j < _input[i].Length; j++)
            {
                _list[i][j] = _input[i][j] == '1';
            }
        }

        return (GetValue(_list, 0, true).GetDecimalNumber() * GetValue(_list, 0, false).GetDecimalNumber()).ToString();
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
