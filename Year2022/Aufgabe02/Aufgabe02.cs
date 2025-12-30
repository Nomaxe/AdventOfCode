using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2022, 2);
    }

    public string Calc()
    {
        int score = 0;

        foreach (var line in _input)
        {
            switch (line[2])
            {
                case 'X':
                    score += 1;
                    switch (line[0])
                    {
                        case 'A':
                            score += 3;
                            break;
                        case 'C':
                            score += 6;
                            break;
                    }
                    break;
                case 'Y':
                    score += 2;
                    switch (line[0])
                    {
                        case 'B':
                            score += 3;
                            break;
                        case 'A':
                            score += 6;
                            break;
                    }
                    break;
                case 'Z':
                    score += 3;
                    switch (line[0])
                    {
                        case 'C':
                            score += 3;
                            break;
                        case 'B':
                            score += 6;
                            break;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return score.ToString();
    }
}
