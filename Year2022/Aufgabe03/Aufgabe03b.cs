using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2022, 3);
    }

    public string Calc()
    {
        int score = 0;

        for (int i = 0; i < _input.Length; i += 3)
        {
            var character = _input[i].Intersect(_input[i + 1]).Intersect(_input[i + 2]).First();

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
