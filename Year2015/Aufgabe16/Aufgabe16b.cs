using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe16b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe16b()
    {
        _input = Utilities.ReadInput(2015, 16);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');

            if (!Check(split[2][..^1], int.Parse(split[3][..^1])))
            {
                continue;
            }

            if (!Check(split[4][..^1], int.Parse(split[5][..^1])))
            {
                continue;
            }

            if (!Check(split[6][..^1], int.Parse(split[7])))
            {
                continue;
            }

            return split[1][..^1];
        }

        throw new NotImplementedException();
    }

    private static bool Check(string compound, int value)
    {
        return compound switch
        {
            "children" => value == 3,
            "cats" => value > 7,
            "samoyeds" => value == 2,
            "pomeranians" => value < 3,
            "akitas" => value == 0,
            "vizslas" => value == 0,
            "goldfish" => value < 5,
            "trees" => value > 3,
            "cars" => value == 2,
            "perfumes" => value == 1,
            _ => throw new NotImplementedException()
        };
    }
}
