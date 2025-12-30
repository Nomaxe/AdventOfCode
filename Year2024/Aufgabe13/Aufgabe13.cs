using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe13 : IAufgabe
{
    private string[] _input;

    public Aufgabe13()
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
            xPrize = Convert.ToUInt64(matches[0].Value);
            yPrize = Convert.ToUInt64(matches[1].Value);

            for (ulong a = 0; a <= 100; a++)
            {
                for (ulong b = 0; b <= 100; b++)
                {
                    var xCurrent = a * xButton1 + b * xButton2;
                    var yCurrent = a * yButton1 + b * yButton2;
                    if (xCurrent == xPrize && yCurrent == yPrize)
                    {
                        result += a * 3 + b;
                    }
                }
            }

            _input = _input.Skip(4).ToArray();
        }

        return result.ToString();
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
