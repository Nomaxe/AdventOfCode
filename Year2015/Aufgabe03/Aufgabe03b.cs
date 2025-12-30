using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe03b : IAufgabe
{
    private readonly string _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2015, 3)[0];
    }

    public string Calc()
    {
        Point currentPointSanta = new(0, 0);
        Point currentPointRobot = new(0, 0);
        bool moveSanta = true;
        HashSet<Point> points = [currentPointSanta];

        foreach (var character in _input)
        {
            if (moveSanta)
            {
                currentPointSanta = Move(currentPointSanta, character);
                points.Add(currentPointSanta);
            }
            else
            {
                currentPointRobot = Move(currentPointRobot, character);
                points.Add(currentPointRobot);
            }

            moveSanta = !moveSanta;
        }

        return points.Count.ToString();
    }

    private static Point Move(Point point, char character)
    {
        return character switch
        {
            '>' => new(point.X + 1, point.Y),
            'v' => new(point.X, point.Y + 1),
            '<' => new(point.X - 1, point.Y),
            '^' => new(point.X, point.Y - 1),
            _ => throw new NotImplementedException()
        };
    }
}
