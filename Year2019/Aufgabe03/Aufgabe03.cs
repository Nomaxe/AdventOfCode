using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe03 : IAufgabe
{
    private readonly string[] _input;
    private DictionaryHashSet<Axis, (Point From, Point To)> _wire;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2019, 3);
        _wire = new(2);
    }

    public string Calc()
    {
        Point currentPoint = new();

        var split = _input[0].Split(',');
        foreach (var action in split)
        {
            var number = int.Parse(action[1..]);
            Point newPoint;
            switch (action[0])
            {
                case 'R':
                    newPoint = new(currentPoint.X + number, currentPoint.Y);
                    _wire.Add(Axis.Horizontal, (currentPoint, newPoint));
                    break;
                case 'D':
                    newPoint = new(currentPoint.X, currentPoint.Y + number);
                    _wire.Add(Axis.Vertical, (currentPoint, newPoint));
                    break;
                case 'L':
                    newPoint = new(currentPoint.X - number, currentPoint.Y);
                    _wire.Add(Axis.Horizontal, (newPoint, currentPoint));
                    break;
                case 'U':
                    newPoint = new(currentPoint.X, currentPoint.Y - number);
                    _wire.Add(Axis.Vertical, (newPoint, currentPoint));
                    break;
                default:
                    throw new NotImplementedException();

            }
            currentPoint = newPoint;
        }

        currentPoint = new();
        int minDistance = int.MaxValue;
        split = _input[1].Split(","); //we assume only 2 wires
        foreach (var action in split)
        {
            Axis axis;
            Point point1;
            Point point2;
            Point newPoint;
            var number = int.Parse(action[1..]);
            switch (action[0])
            {
                case 'R':
                    newPoint = new(currentPoint.X + number, currentPoint.Y);
                    axis = Axis.Vertical;
                    point1 = currentPoint;
                    point2 = newPoint;
                    break;
                case 'D':
                    newPoint = new(currentPoint.X, currentPoint.Y + number);
                    axis = Axis.Horizontal;
                    point1 = currentPoint;
                    point2 = newPoint;
                    break;
                case 'L':
                    newPoint = new(currentPoint.X - number, currentPoint.Y);
                    axis = Axis.Vertical;
                    point1 = newPoint;
                    point2 = currentPoint;
                    break;
                case 'U':
                    newPoint = new(currentPoint.X, currentPoint.Y - number);
                    axis = Axis.Horizontal;
                    point1 = newPoint;
                    point2 = currentPoint;
                    break;
                default:
                    throw new NotImplementedException();

            }
            currentPoint = newPoint;

            IEnumerable<Point> intersections = axis switch
            {
                Axis.Vertical => _wire[axis].Where(x => x.From.Y < point1.Y && x.To.Y > point1.Y &&
                                                        x.From.X > point1.X && x.To.X < point2.X)
                                            .Select(x => new Point(x.From.X, point1.Y)),
                Axis.Horizontal => _wire[axis].Where(x => x.From.X < point1.X && x.To.X > point1.X &&
                                                          x.From.Y > point1.Y && x.To.Y < point2.Y)
                                              .Select(x => new Point(point1.X, x.From.Y)),
                _ => throw new NotImplementedException(),
            };

            foreach (var intersection in intersections)
            {
                minDistance = int.Min(minDistance, intersection.GetManhattenDistanceToZero());
            }
        }

        return minDistance.ToString();
    }
}
