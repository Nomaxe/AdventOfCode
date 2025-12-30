using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe25 : IAufgabe
{
    private readonly List<int[]> _locks = [];
    private readonly List<int[]> _keys = [];

    public Aufgabe25()
    {
        List<string> currentInput = [];
        var input = Utilities.ReadInput(2024, 25);

        foreach (var line in input.Append(string.Empty))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                var values = GetCount(currentInput);
                if (currentInput[0][0] == '#')
                {
                    _locks.Add(values);
                }
                else
                {
                    _keys.Add(values);
                }

                currentInput.Clear();
            }
            else
            {
                currentInput.Add(line);
            }
        }
    }

    public string Calc()
    {
        ulong result = 0;

        foreach (var @lock in _locks)
        {
            foreach (var key in _keys)
            {
                if (SumSmaller(@lock, key))
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    private static int[] GetCount(List<string> input)
    {
        int[] values =
        [
            input.Count(x => x[0] == '#') - 1,
            input.Count(x => x[1] == '#') - 1,
            input.Count(x => x[2] == '#') - 1,
            input.Count(x => x[3] == '#') - 1,
            input.Count(x => x[4] == '#') - 1,
        ];

        return values;
    }

    private static bool SumSmaller(int[] @lock, int[] key)
    {
        for (int i = 0; i < @lock.Length; i++)
        {
            if (@lock[i] + key[i] >= 6)
            {
                return false;
            }
        }

        return true;
    }
}
