namespace AdventOfCode.Utils;

internal static class MathEnhancement
{
    public static int GetHighestCommonDivisor(int a, int b)
    {
        a = int.Abs(a);
        b = int.Abs(b);

        while (a != 0 && b != 0)
        {
            if (a > b)
            {
                a %= b;
            }
            else
            {
                b %= a;
            }
        }

        return a | b;
    }

    public static ulong GetHighestCommonDivisor(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
            {
                a %= b;
            }
            else
            {
                b %= a;
            }
        }

        return a | b;
    }

    public static ulong GetLowestCommonMultiple(IEnumerable<ulong> numbers)
    {
        ulong result = 1;

        foreach (var number in numbers)
        {
            result *= number / GetHighestCommonDivisor(result, number);
        }

        return result;
    }
}
