using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe17 : IAufgabe
{
    private readonly string _input;
    private Point _targetStart;
    private Point _targetEnd;
    private int _highestPoint;

    public Aufgabe17()
    {
        _input = Utilities.ReadInputAsString(2021, 17);
        _highestPoint = 0;
    }

    public string Calc()
    {
        var numbers = _input.GetNumbers();
        _targetStart = new(numbers[0], numbers[3]);
        _targetEnd = new(numbers[1], numbers[2]);

        for (int x = 0; x < 150; x++)
        {
            for (int y = 0; y < 150; y++)
            {
                Check(x, y);
            }
        }

        return _highestPoint.ToString();
    }

    private void Check(int x, int y)
    {
        Point currentPosition = new(0, 0);
        var highestPoint = 0;

        do
        {
            currentPosition = currentPosition.Move(x, y);
            if (x > 0)
            {
                x--;
            }
            y--;

            if (currentPosition.Y > highestPoint)
            {
                highestPoint = currentPosition.Y;
            }

            if (currentPosition.X >= _targetStart.X && currentPosition.X <= _targetEnd.X && currentPosition.Y <= _targetStart.Y && currentPosition.Y >= _targetEnd.Y)
            {
                if (highestPoint > _highestPoint)
                {
                    _highestPoint = highestPoint;
                }

                return;
            }
        } while (currentPosition.X <= _targetEnd.X && currentPosition.Y >= _targetEnd.Y);
    }
}
