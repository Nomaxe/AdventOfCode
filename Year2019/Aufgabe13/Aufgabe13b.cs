using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe13b : IAufgabe
{
    private readonly IntCode _intcode;

    public Aufgabe13b()
    {
        _intcode = new(2019, 13)
        {
            WaitOnInput = true
        };
        _intcode.SetCode(0, 2);
    }

    public string Calc()
    {
        long score = 0;
        long lastScore = 0;
        int movesWithoutScoreUpdate = 0;

        while (movesWithoutScoreUpdate < 2000)
        {
            long posXPaddle = 0;
            long posXBall = 0;

            _intcode.Calc();

            for (int i = 0; i < _intcode.Out.Count; i += 3)
            {
                if (_intcode.Out[i] == -1 && _intcode.Out[i + 1] == 0)
                {
                    score = _intcode.Out[i + 2];
                    continue;
                }

                switch (_intcode.Out[i + 2])
                {
                    case 3:
                        posXPaddle = _intcode.Out[i];
                        break;
                    case 4:
                        posXBall = _intcode.Out[i];
                        break;
                }
            }

            if (score != lastScore)
            {
                lastScore = score;
                movesWithoutScoreUpdate = 0;
            }
            else
            {
                movesWithoutScoreUpdate++;
            }

            if (posXPaddle > posXBall)
            {
                _intcode.AddInput(-1);
            }
            else if (posXBall > posXPaddle)
            {
                _intcode.AddInput(1);
            }
            else
            {
                _intcode.AddInput(0);
            }

            _intcode.ClearOut();
        }

        return score.ToString();
    }
}
