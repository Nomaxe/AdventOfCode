using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe17b : IAufgabe
{
    private readonly string _input;
    private Point _targetStart;
    private Point _targetEnd;

    public Aufgabe17b()
    {
        _input = Utilities.ReadInputAsString(2021, 17);
    }

    public string Calc()
    {
        //Optimization: https://www.reddit.com/r/adventofcode/comments/rily4v/2021_day_17_part_2_never_brute_force_when_you_can/

        var numbers = _input.GetNumbers();
        _targetStart = new(numbers[0], numbers[3]);
        _targetEnd = new(numbers[1], numbers[2]);

        int count = 0;

        for (int x = -0; x < 300; x++)
        {
            for (int y = -150; y < 150; y++)
            {
                if (Check(x, y))
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }

    private bool Check(int x, int y)
    {
        Point currentPosition = new(0, 0);

        do
        {
            currentPosition = currentPosition.Move(x, y);
            if (x > 0)
            {
                x--;
            }
            y--;

            if (currentPosition.X >= _targetStart.X && currentPosition.X <= _targetEnd.X && currentPosition.Y <= _targetStart.Y && currentPosition.Y >= _targetEnd.Y)
            {
                return true;
            }
        } while (currentPosition.X <= _targetEnd.X && currentPosition.Y >= _targetEnd.Y);

        return false;
    }
}
