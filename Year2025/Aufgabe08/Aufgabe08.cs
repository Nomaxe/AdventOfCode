using AdventOfCode.Utils;
using System.Collections;

namespace AdventOfCode.Year2025;

internal class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;
    private readonly Sorter _sorted;

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2025, 8);
        _sorted = new();
    }

    public string Calc()
    {
        List<Point3D> points = new(_input.Length);
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
                _sorted.Add(distance, points[i], points[j]);
            }
        }

        foreach (var sorted in _sorted)
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
        }

        var result = 1;
        foreach (var group in connections.GroupBy(x => x.Value).OrderByDescending(x => x.Count()).Take(3))
        {
            result *= group.Count();
        }

        return result.ToString();
    }

    private class Sorter : IEnumerable<SortElement>
    {
        private readonly SortElement[] _sortArray;
        private int _count = 0;

        public Sorter()
        {
            _sortArray = new SortElement[1000];
        }

        public void Add(double distance, Point3D point1, Point3D point2)
        {
            if (_count < 1000)
            {
                int insertIndex = 0;

                for (int i = 0; i < _count; i++)
                {
                    if (distance < _sortArray[i].Distance)
                    {
                        insertIndex = i;
                        break;
                    }
                }

                Array.Copy(_sortArray, insertIndex, _sortArray, insertIndex + 1, _count - insertIndex);
                _sortArray[insertIndex] = new(distance, point1, point2);
                _count++;
            }
            else
            {
                var insertIndex = -1;

                for (int i = 0; i < _count; i++)
                {
                    if (distance < _sortArray[i].Distance)
                    {
                        insertIndex = i;
                        break;
                    }
                }

                if (insertIndex == -1)
                {
                    return;
                }

                Array.Copy(_sortArray, insertIndex, _sortArray, insertIndex + 1, _count - insertIndex - 1);
                _sortArray[insertIndex] = new(distance, point1, point2);
            }
        }

        public IEnumerator<SortElement> GetEnumerator()
        {
            return ((IEnumerable<SortElement>)_sortArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sortArray.GetEnumerator();
        }
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
