using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private readonly int[] _presentSize;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2025, 12);
        _presentSize = new int[6]; //assume there are always 6 types
    }

    public string Calc()
    {
        _presentSize[0] += GetCount(1);
        _presentSize[0] += GetCount(2);
        _presentSize[0] += GetCount(3);

        _presentSize[1] += GetCount(6);
        _presentSize[1] += GetCount(7);
        _presentSize[1] += GetCount(8);

        _presentSize[2] += GetCount(11);
        _presentSize[2] += GetCount(12);
        _presentSize[2] += GetCount(13);

        _presentSize[3] += GetCount(16);
        _presentSize[3] += GetCount(17);
        _presentSize[3] += GetCount(18);

        _presentSize[4] += GetCount(21);
        _presentSize[4] += GetCount(22);
        _presentSize[4] += GetCount(23);

        _presentSize[5] += GetCount(26);
        _presentSize[5] += GetCount(27);
        _presentSize[5] += GetCount(28);

        var result = 0;

        for (int i = 30; i < _input.Length; i++)
        {
            var numbers = _input[i].GetUnsignedNumbers();
            var size = numbers[0] * numbers[1];

            var spaceNeeded = 0;

            for (int j = 2; j < numbers.Length; j++)
            {
                spaceNeeded += numbers[j] * _presentSize[j - 2];
            }

            if (spaceNeeded < size)
            {
                result++;
            }
        }

        return result.ToString();
    }

    private int GetCount(int line)
    {
        return _input[line].Count(x => x == '#');
    }
}
