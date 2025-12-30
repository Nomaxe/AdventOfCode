using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe02 : IAufgabe
{
    private readonly string _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInputAsString(2025, 2);
    }

    public string Calc()
    {
        var split = _input.Split(',');
        long result = 0;

        foreach (var range in split)
        {
            var rangeSplit = range.Split('-');
            var index1 = long.Parse(rangeSplit[0]);
            var index2 = long.Parse(rangeSplit[1]);

            for (long i = index1; i <= index2; i++)
            {
                var number = i.ToString();

                if (number.Length % 2 == 0)
                {
                    var firstPart = number[..(number.Length / 2)];
                    var secondPart = number[(number.Length / 2)..];

                    if (firstPart == secondPart)
                    {
                        result += i;
                    }
                }
            }
        }

        return result.ToString();
    }
}
