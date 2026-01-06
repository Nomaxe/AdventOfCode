using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<long, long> _memory;

    public Aufgabe14()
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
                    for (int i = mask.Length - 1; i >= 0; i--)
                    {
                        switch (mask[i])
                        {
                            case '0':
                                number &= ~(1L << (mask.Length - i - 1));
                                break;
                            case '1':
                                number |= 1L << (mask.Length - i - 1);
                                break;
                        }
                    }
                    _memory[numbers[0]] = number;
                    break;
            }
        }

        return _memory.Values.Sum().ToString();
    }
}
