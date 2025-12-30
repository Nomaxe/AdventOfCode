using AdventOfCode.Utils;
using AdventOfCode.Year2018.Namespace16;

namespace AdventOfCode.Year2018;

internal class Aufgabe16 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe16()
    {
        _input = Utilities.ReadInput(2018, 16);
    }

    public string Calc()
    {
        int result = 0;
        Device device = new();

        for (int i = 0; i < _input.Length; i += 4)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                return result.ToString();
            }

            var split1 = _input[i][9..^1].Split(',');
            var split2 = _input[i + 1].Split(' ');
            var split3 = _input[i + 2][9..^1].Split(',');
            var possibleOpcodes = device.GetPossibleOpcodes(int.Parse(split2[1]), int.Parse(split2[2]), int.Parse(split2[3]),
                                                            int.Parse(split1[0]), int.Parse(split1[1]), int.Parse(split1[2]), int.Parse(split1[3]),
                                                            int.Parse(split3[0]), int.Parse(split3[1]), int.Parse(split3[2]), int.Parse(split3[3])).Count;

            if (possibleOpcodes >= 3)
            {
                result++;
            }
        }

        throw new NotImplementedException();
    }
}
