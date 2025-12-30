using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe03 : IAufgabe
{
    private readonly string _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2015, 3)[0];
    }

    public string Calc()
    {
        Point currentPoint = new(0, 0);
        HashSet<Point> points = [currentPoint];

        foreach (var character in _input)
        {
            currentPoint = character switch
            {
                '>' => new(currentPoint.X + 1, currentPoint.Y),
                'v' => new(currentPoint.X, currentPoint.Y + 1),
                '<' => new(currentPoint.X - 1, currentPoint.Y),
                '^' => new(currentPoint.X, currentPoint.Y - 1),
                _ => throw new NotImplementedException()
            };

            points.Add(currentPoint);
        }

        return points.Count.ToString();
    }
}
