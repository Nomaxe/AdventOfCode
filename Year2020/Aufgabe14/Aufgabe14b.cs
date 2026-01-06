using AdventOfCode.Utils;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020;

internal class Aufgabe14b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<long, long> _memory;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInput(2020, 14);
        _memory = new();
    }

    public string Calc()
    {
        string mask = "";

        foreach (var line in _input)
        {
            switch (line[1])
            {
                case 'a': //mask
                    mask = line[7..];
                    break;
                case 'e': //mem
                    var numbers = line.GetUnsignedLongNumbers();
                    var number = numbers[1];
                    foreach (var address in GetMemoryAddress(numbers[0], mask))
                    {
                        _memory[address] = number;
                    }
                    break;
            }
        }

        return _memory.Values.Sum().ToString();
    }

    private static List<long> GetMemoryAddress(long number, string mask)
    {
        List<long> list = [number];

        for (int i = 0; i < mask.Length; i++)
        {
            switch (mask[i])
            {
                case '1':
                    OverwriteIndexWith1(list, mask.Length - i - 1);
                    break;
                case 'X':
                    AppendXValues(list, mask.Length - i - 1);
                    break;
            }
        }

        return list;
    }

    private static void OverwriteIndexWith1(List<long> list, int offset)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] |= 1L << offset;
        }
    }

    private static void AppendXValues(List<long> list, int offset)
    {
        var number = 1L << offset;
        var count = list.Count;

        for (int i = 0; i < count; i++)
        {
            list.Add(list[i] ^ number);
        }
    }
}
