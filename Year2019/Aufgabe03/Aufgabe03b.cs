using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryHashSet<Axis, (Point From, Point To, int Distance, Direction Direction)> _wire;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2019, 3);
        _wire = new(2);
    }

    public string Calc()
    {
        Point currentPoint = new();

        var split = _input[0].Split(',');
        int distance = 0;
        foreach (var action in split)
        {
            var number = int.Parse(action[1..]);
            Point newPoint;
            switch (action[0])
            {
                case 'R':
                    newPoint = new(currentPoint.X + number, currentPoint.Y);
                    _wire.Add(Axis.Horizontal, (currentPoint, newPoint, distance, Direction.Right));
                    break;
                case 'D':
                    newPoint = new(currentPoint.X, currentPoint.Y + number);
                    _wire.Add(Axis.Vertical, (currentPoint, newPoint, distance, Direction.Down));
                    break;
                case 'L':
                    newPoint = new(currentPoint.X - number, currentPoint.Y);
                    _wire.Add(Axis.Horizontal, (newPoint, currentPoint, distance, Direction.Left));
                    break;
                case 'U':
                    newPoint = new(currentPoint.X, currentPoint.Y - number);
                    _wire.Add(Axis.Vertical, (newPoint, currentPoint, distance, Direction.Up));
                    break;
                default:
                    throw new NotImplementedException();

            }
            distance += number;
            currentPoint = newPoint;
        }

        currentPoint = new();
        distance = 0;
        int minDistance = int.MaxValue;
        split = _input[1].Split(","); //we assume only 2 wires
        foreach (var action in split)
        {
            Axis axis;
            Direction direction;
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
                    direction = Direction.Right;
                    break;
                case 'D':
                    newPoint = new(currentPoint.X, currentPoint.Y + number);
                    axis = Axis.Horizontal;
                    point1 = currentPoint;
                    point2 = newPoint;
                    direction = Direction.Down;
                    break;
                case 'L':
                    newPoint = new(currentPoint.X - number, currentPoint.Y);
                    axis = Axis.Vertical;
                    point1 = newPoint;
                    point2 = currentPoint;
                    direction = Direction.Left;
                    break;
                case 'U':
                    newPoint = new(currentPoint.X, currentPoint.Y - number);
                    axis = Axis.Horizontal;
                    point1 = newPoint;
                    point2 = currentPoint;
                    direction = Direction.Up;
                    break;
                default:
                    throw new NotImplementedException();

            }
            currentPoint = newPoint;

            var intersectionsDistance = axis switch
            {
                Axis.Vertical => _wire[axis].Where(x => x.From.Y < point1.Y && x.To.Y > point1.Y &&
                                                        x.From.X > point1.X && x.To.X < point2.X)
                                            .Select(x => GetDistance(x.From, x.To, new(x.From.X, point1.Y), x.Distance, x.Direction) +
                                                         GetDistance(point1, point2, new(x.From.X, point1.Y), distance, direction)),
                Axis.Horizontal => _wire[axis].Where(x => x.From.X < point1.X && x.To.X > point1.X &&
                                                          x.From.Y > point1.Y && x.To.Y < point2.Y)
                                              .Select(x => GetDistance(x.From, x.To, new(point1.X, x.From.Y), x.Distance, x.Direction) +
                                                           GetDistance(point1, point2, new(point1.X, x.From.Y), distance, direction)),
                _ => throw new NotImplementedException(),
            };

            foreach (var intersectionDistance in intersectionsDistance)
            {
                minDistance = int.Min(minDistance, intersectionDistance);
            }

            distance += number;
        }

        return minDistance.ToString();
    }

    private static int GetDistance(Point corner1, Point corner2, Point intersection, int distanceToCorner, Direction direction)
    {
        return distanceToCorner + direction switch
        {
            Direction.Right => intersection.X - corner1.X,
            Direction.Down => intersection.Y - corner1.Y,
            Direction.Left => corner2.X - intersection.X,
            Direction.Up => corner2.Y - intersection.Y,
            _ => throw new NotImplementedException(),
        };
    }
}
