using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe21b : IAufgabe
{
    private readonly int _magicNumber;

    public Aufgabe21b()
    {
        _magicNumber = int.Parse(Utilities.ReadInput(2018, 21)[8].Split(' ')[1]);
    }

    public string Calc()
    {
        int number = GetNextNumber(0);
        HashSet<int> values = new();

        while (true)
        {
            var nextNumber = GetNextNumber(number);
            if (!values.Add(nextNumber))
            {
                return number.ToString();
            }

            number = nextNumber;
        }
    }

    private int GetNextNumber(int a)
    {
        a |= 65536;
        int b = _magicNumber;
        b += a & 255;
        b &= 268435455;
        b *= 65899;
        b &= 268435455;
        b += (a >> 8) & 255;
        b &= 268435455;
        b *= 65899;
        b &= 268435455;
        b += (a >> 16) & 255;
        b &= 268435455;
        b *= 65899;
        b &= 16777215;
        return b;
    }
}