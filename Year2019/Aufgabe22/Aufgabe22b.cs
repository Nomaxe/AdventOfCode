using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Year2019;

internal class Aufgabe22b : IAufgabe
{
    private const ulong ArrayLength = 119315717514047;
    private readonly string[] _input;

    public Aufgabe22b()
    {
        _input = Utilities.ReadInput(2019, 22);
    }

    public string Calc()
    {
        const ulong Times = 101741582076661;
        BigInteger increment_mul = BigInteger.One;
        BigInteger offset_diff = BigInteger.Zero;

        foreach (var line in _input)
        {
            if (line[0] == 'c') //cut
            {
                var number = int.Parse(line[4..]);
                offset_diff += number * increment_mul;
            }
            else if (line[5] == 'i') //deal into new stack
            {
                increment_mul = -increment_mul;
                offset_diff += increment_mul;
            }
            else //deal with increment
            {
                var number = int.Parse(line[20..]);
                increment_mul *= BigInteger.ModPow(number, ArrayLength - 2, ArrayLength);
            }

            increment_mul %= ArrayLength;
            offset_diff %= ArrayLength;
        }
 
        var increment = BigInteger.ModPow(increment_mul, Times, ArrayLength);
        var offset = offset_diff * (1 - increment) * BigInteger.ModPow(1 - increment_mul, ArrayLength - 2, ArrayLength);
        offset %= ArrayLength;

        return ((offset + 2020 * increment) % ArrayLength).ToString();
    }
} 