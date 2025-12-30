using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe15b : IAufgabe
{
    private ulong _generatorA;
    private ulong _generatorB;
    private readonly Queue<ulong> _queueA = [];
    private readonly Queue<ulong> _queueB = [];

    public Aufgabe15b()
    {
        var input = Utilities.ReadInput(2017, 15);
        _generatorA = ulong.Parse(input[0][24..]);
        _generatorB = ulong.Parse(input[1][24..]);
    }

    public string Calc()
    {
        int result = 0;
        int compare = 0;

        do
        {
            _generatorA = GenerateNextNumber(_generatorA, 16807);
            _generatorB = GenerateNextNumber(_generatorB, 48271);

            if (_generatorA % 4 == 0)
            {
                _queueA.Enqueue(_generatorA);
            }
            if (_generatorB % 8 == 0)
            {
                _queueB.Enqueue(_generatorB);
            }

            if (_queueA.Count > 1 && _queueB.Count > 1)
            {
                compare++;
                if (_queueA.Dequeue() % 0x1_0000 == _queueB.Dequeue() % 0x1_0000)
                {
                    result++;
                }
            }
        } while (compare < 5_000_000);

        return result.ToString();
    }

    private static ulong GenerateNextNumber(ulong current, ulong factor)
    {
        current *= factor;
        return current % 2_147_483_647;
    }
}
