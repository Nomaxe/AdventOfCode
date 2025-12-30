using AdventOfCode.Utils;
using System.Collections;

namespace AdventOfCode.Year2025;

internal class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08b()
    {
        _input = Utilities.ReadInput(2025, 8);
    }

    public string Calc()
    {
        //Optimierung: https://en.wikipedia.org/wiki/Minimum_spanning_tree

        List<Point3D> points = new(_input.Length);
        List<SortElement> sortedPoints = new(_input.Length);
        Dictionary<Point3D, int> connections = new(_input.Length);
        var connectionId = 1;

        foreach (var line in _input)
        {
            Point3D point = new(line);
            points.Add(point);
            connections.Add(point, connectionId);
            connectionId++;
        }

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var distance = points[i].GetStraighLineDistance(points[j]);
                sortedPoints.Add(new(distance, points[i], points[j]));
            }
        }
        sortedPoints = sortedPoints.OrderBy(x => x.Distance).ToList();

        var connectionCount = 1000;
        foreach (var sorted in sortedPoints)
        {
            var firstConnection = connections[sorted.Point1];
            var secondConnection = connections[sorted.Point2];

            if (firstConnection == secondConnection)
            {
                continue;
            }

            foreach (var connection in connections.Where(x => x.Value == secondConnection))
            {
                connections[connection.Key] = firstConnection;
            }

            connectionCount--;

            if (connectionCount == 1)
            {
                return (sorted.Point1.X * (long)sorted.Point2.X).ToString();
            }
        }

        throw new NotImplementedException();
    }

    private class SortElement
    {
        public double Distance { get; private init; }
        public Point3D Point1 { get; private init; }
        public Point3D Point2 { get; private init; }

        public SortElement(double distance, Point3D point1, Point3D point2)
        {
            Distance = distance;
            Point1 = point1;
            Point2 = point2;
        }

        public override string ToString()
        {
            return $"{Distance:N2}";
        }
    }
}
