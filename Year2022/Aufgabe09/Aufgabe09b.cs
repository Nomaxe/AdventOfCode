using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe09b : IAufgabe
{
    private readonly string[] _input;
    private readonly Point[] _rope;

    public Aufgabe09b()
    {
        _input = Utilities.ReadInput(2022, 9);
        _rope = new Point[10];
    }

    public string Calc()
    {
        HashSet<Point> visitedPoints = [_rope[^1]];

        foreach (var line in _input)
        {
            var direction = line[0].ToDirection();
            var number = int.Parse(line[2..]);

            for (int i = 0; i < number; i++)
            {
                _rope[0] = _rope[0].Move(direction, 1);

                int j = 1;
                for (; j < _rope.Length; j++)
                {
                    int x = _rope[j - 1].X;
                    int y = _rope[j - 1].Y;
                    bool xOk = false;
                    bool yOk = false;

                    if (_rope[j].X - _rope[j - 1].X >= 2)
                    {
                        x++;
                    }
                    else if (_rope[j].X - _rope[j - 1].X <= -2)
                    {
                        x--;
                    }
                    else
                    {
                        xOk = true;
                    }
                    if (_rope[j].Y - _rope[j - 1].Y >= 2)
                    {
                        y++;
                    }
                    else if (_rope[j].Y - _rope[j - 1].Y <= -2)
                    {
                        y--;
                    }
                    else
                    {
                        yOk = true;
                    }

                    if (xOk && yOk)
                    {
                        break;
                    }

                    _rope[j] = new(x, y);
                }

                if (j == _rope.Length)
                {
                    visitedPoints.Add(_rope[^1]);
                }
            }
        }

        return visitedPoints.Count.ToString();
    }
}
