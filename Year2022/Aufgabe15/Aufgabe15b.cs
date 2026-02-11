using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe15b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<(Point Sensor, int Distance)> _beacons;
    private const long CheckGrid = 4_000_000;

    public Aufgabe15b()
    {
        _input = Utilities.ReadInput(2022, 15);
        _beacons = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.GetNumbers();
            _beacons.Add((new(split[0], split[1]), new Point(split[0], split[1]).GetManhattenDistance(new(split[2], split[3]))));
        }

        var point = CalcPoint();

        return (point.X * CheckGrid + point.Y).ToString();
    }

    private Point CalcPoint()
    {
        HashSet<Point> points = [];
        Point point;

        foreach (var beacon in _beacons)
        {
            for (int i = 0; i <= beacon.Distance; i++)
            {
                var otherSide = beacon.Distance - i;

                point = new(beacon.Sensor.X + i, beacon.Sensor.Y - otherSide - 1);
                if (IsValidPoint(point))
                {
                    points.Add(point);
                }

                point = new(beacon.Sensor.X + otherSide + 1, beacon.Sensor.Y + i);
                if (IsValidPoint(point))
                {
                    points.Add(point);
                }

                point = new(beacon.Sensor.X - i, beacon.Sensor.Y + otherSide + 1);
                if (IsValidPoint(point))
                {
                    points.Add(point);
                }

                point = new(beacon.Sensor.X - otherSide - 1, beacon.Sensor.Y - i);
                if (IsValidPoint(point))
                {
                    points.Add(point);
                }
            }
        }

        foreach (var checkPoint in points)
        {
            if (_beacons.All(x => checkPoint.GetManhattenDistance(x.Sensor) > x.Distance))
            {
                return checkPoint;
            }
        }

        throw new NotImplementedException();
    }

    private static bool IsValidPoint(Point point)
    {
        if (point.X < 0 || point.Y < 0)
        {
            return false;
        }

        if (point.X > CheckGrid || point.Y > CheckGrid)
        {
            return false;
        }

        return true;
    }
}
