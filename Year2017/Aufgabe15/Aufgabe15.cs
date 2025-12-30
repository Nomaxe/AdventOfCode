using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe15 : IAufgabe
{
    private ulong _generatorA;
    private ulong _generatorB;


    public Aufgabe15()
    {
        var input = Utilities.ReadInput(2017, 15);
        _generatorA = ulong.Parse(input[0][24..]);
        _generatorB = ulong.Parse(input[1][24..]);
    }

    public string Calc()
    {
        int result = 0;

        for (int i = 0; i < 40_000_000; i++)
        {
            _generatorA = GenerateNextNumber(_generatorA, 16807);
            _generatorB = GenerateNextNumber(_generatorB, 48271);

            if (_generatorA % 0x1_0000 == _generatorB % 0x1_0000)
            {
                result++;
            }
        }

        return result.ToString();
    }

    private static ulong GenerateNextNumber(ulong current, ulong factor)
    {
        current *= factor;
        return current % 2_147_483_647;
    }
}
