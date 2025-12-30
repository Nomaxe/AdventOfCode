using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe03 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2022, 3);
    }

    public string Calc()
    {
        int score = 0;

        foreach (var line in _input)
        {
            var character = line[..(line.Length / 2)].Intersect(line[(line.Length / 2)..]).First();

            if (char.IsLower(character))
            {
                score += character - 'a' + 1;
            }
            else
            {
                score += character - 'A' + 27;
            }
        }

        return score.ToString();
    }
}
