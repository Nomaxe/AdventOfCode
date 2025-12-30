using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe02b : IAufgabe
{
    private readonly string _input;

    public Aufgabe02b()
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

                for (int j = 1; j <= number.Length / 2; j++)
                {
                    if (number.Length % j != 0)
                    {
                        continue;
                    }

                    var checkPart = number[..j];
                    var checkSuccessfull = true;
                    for (int k = j; k < number.Length; k += j)
                    {
                        if (checkPart != number[k..(k + j)])
                        {
                            checkSuccessfull = false;
                            break;
                        }
                    }

                    if (checkSuccessfull)
                    {
                        result += i;
                        break;
                    }
                }
            }
        }

        return result.ToString();
    }
}
