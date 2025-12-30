using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2022, 2);
    }

    public string Calc()
    {
        const int WinScore = 6;
        const int DrawScore = 3;
        const int LossScore = 0;
        const int RockScore = 1;
        const int PaperScore = 2;
        const int ScissorsScore = 3;

        int score = 0;

        foreach (var line in _input)
        {
            score += line[0] switch
            {
                'A' => line[2] switch
                {
                    'X' => LossScore + ScissorsScore,
                    'Y' => DrawScore + RockScore,
                    'Z' => WinScore + PaperScore,
                    _ => throw new NotImplementedException(),
                },
                'B' => line[2] switch
                {
                    'X' => LossScore + RockScore,
                    'Y' => DrawScore + PaperScore,
                    'Z' => WinScore + ScissorsScore,
                    _ => throw new NotImplementedException(),
                },
                'C' => line[2] switch
                {
                    'X' => LossScore + PaperScore,
                    'Y' => DrawScore + ScissorsScore,
                    'Z' => WinScore + RockScore,
                    _ => throw new NotImplementedException(),
                },
                _ => throw new NotImplementedException(),
            };
        }

        return score.ToString();
    }
}
