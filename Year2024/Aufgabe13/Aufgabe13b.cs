using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe13b : IAufgabe
{
    private string[] _input;

    public Aufgabe13b()
    {
        _input = Utilities.ReadInput(2024, 13);
    }

    public string Calc()
    {
        ulong xButton1, xButton2;
        ulong yButton1, yButton2;
        ulong xPrize, yPrize;
        ulong result = 0;

        while (_input.Length > 0)
        {
            Regex regex = NumberRegex();
            var matches = regex.Matches(_input[0]);
            xButton1 = Convert.ToUInt64(matches[0].Value);
            yButton1 = Convert.ToUInt64(matches[1].Value);

            matches = regex.Matches(_input[1]);
            xButton2 = Convert.ToUInt64(matches[0].Value);
            yButton2 = Convert.ToUInt64(matches[1].Value);

            matches = regex.Matches(_input[2]);
            xPrize = Convert.ToUInt64(matches[0].Value) + 10000000000000;
            yPrize = Convert.ToUInt64(matches[1].Value) + 10000000000000;

            decimal xBruch = (decimal)yButton1 / xButton1;
            decimal y = (yPrize - (xBruch * xPrize)) / (yButton2 - (xBruch * xButton2));
            decimal x = (xPrize - xButton2 * y) / xButton1;

            if (IsFlatNumber(y) && IsFlatNumber(x))
            {
                result += Convert.ToUInt64(x) * 3 + Convert.ToUInt64(y);
            }

            _input = _input.Skip(4).ToArray();
        }

        return result.ToString();
    }

    private static bool IsFlatNumber(decimal number)
    {
        var check = number % 1;
        if (check >= (decimal)0.5)
        {
            check = 1 - check;
        }

        return check <= (decimal)0.00001;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
