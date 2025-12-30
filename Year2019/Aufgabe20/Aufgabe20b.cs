using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe20b : IAufgabe
{
    private readonly Grid _grid;
    private readonly Dictionary<Point, Point> _jumpPointsToInner = [];
    private readonly Dictionary<Point, Point> _jumpPointsToOuter = [];
    private Point _startPoint;
    private Point _endPoint;
    private readonly List<GridBool> _visited;

    private const int xFrom = 33;
    private const int xTo = 90;
    private const int yFrom = 33;
    private const int yTo = 90;

    public Aufgabe20b()
    {
        _grid = Grid.CreateCharGrid(2019, 20);
        _visited = new();
    }

    public string Calc()
    {
        GetJumpPoints();
        var length = 0;
        HashSet<DimensionPoint> points = [new(0, _startPoint)];

        while (true)
        {
            HashSet<DimensionPoint> nextPoints = new(points.Count);

            foreach (var point in points)
            {
                AddVisited(point);
                var neighbours = point.GetNeighbours();
                foreach (var neighbour in neighbours)
                {
                    if (_grid.GetValue(neighbour.Point) == '#')
                    {
                        continue;
                    }

                    if (neighbour.Dimension == 0 && neighbour.Point == _endPoint)
                    {
                        return (length + 1).ToString();
                    }

                    int dimension;
                    Point nextPoint;
                    if (_jumpPointsToInner.TryGetValue(neighbour.Point, out var jumpPoint))
                    {
                        dimension = neighbour.Dimension + 1;
                        nextPoint = jumpPoint;
                    }
                    else if (_jumpPointsToOuter.TryGetValue(neighbour.Point, out jumpPoint))
                    {
                        dimension = neighbour.Dimension - 1;
                        if (dimension < 0)
                        {
                            continue;
                        }

                        nextPoint = jumpPoint;
                    }
                    else
                    {
                        dimension = neighbour.Dimension;
                        nextPoint = neighbour.Point;

                        if (_grid.GetValue(nextPoint) != '.')
                        {
                            continue;
                        }
                    }

                    if (DidVisit(dimension, nextPoint))
                    {
                        continue;
                    }

                    nextPoints.Add(new(dimension, nextPoint));
                }
            }

            points = nextPoints;
            length++;
        }

        throw new NotImplementedException();

        //<7176
    }

    private void AddVisited(DimensionPoint point)
    {
        if (_visited.Count == point.Dimension)
        {
            _visited.Add(new(_grid.SizeX, _grid.SizeY));
        }

        _visited[point.Dimension].SetValue(point.Point, true);
    }

    private bool DidVisit(int dimension, Point point)
    {
        if (dimension == _visited.Count)
        {
            return false;
        }

        return _visited[dimension].GetValue(point);
    }

    private void GetJumpPoints()
    {
        DictionaryList<string, Point> points = [];

        //oben & unten
        for (int x = 0; x < _grid.SizeX; x++)
        {
            var value = _grid.GetValue(x, 0);
            if (value != ' ')
            {
                Point point = new(x, 1);
                points.Add($"{value}{_grid.GetValue(point)}", point);
            }

            value = _grid.GetValue(x, _grid.SizeY - 1);
            if (value != ' ')
            {
                Point point = new(x, _grid.SizeY - 2);
                points.Add($"{_grid.GetValue(point)}{value}", point);
            }
        }

        //links & rechts
        for (int y = 0; y < _grid.SizeY; y++)
        {
            var value = _grid.GetValue(0, y);
            if (value != ' ')
            {
                Point point = new(1, y);
                points.Add($"{value}{_grid.GetValue(point)}", point);
            }

            value = _grid.GetValue(_grid.SizeX - 1, y);
            if (value != ' ')
            {
                Point point = new(_grid.SizeX - 2, y);
                points.Add($"{_grid.GetValue(point)}{value}", point);
            }
        }

        //Donut innen - we assume, that the donut is always the same size
        //oben & unten
        for (int x = xFrom; x < xTo; x++)
        {
            var value = _grid.GetValue(x, yFrom);
            if (value != ' ')
            {
                Point point = new(x, yFrom);
                points.Add($"{value}{_grid.GetValue(x, yFrom + 1)}", point);
            }

            value = _grid.GetValue(x, yTo + 1);
            if (value != ' ')
            {
                Point point = new(x, yTo + 1);
                points.Add($"{_grid.GetValue(x, yTo)}{value}", point);
            }
        }

        //links & rechts
        for (int y = yFrom; y < yTo; y++)
        {
            var value = _grid.GetValue(xFrom, y);
            if (value != ' ')
            {
                Point point = new(xFrom, y);
                points.Add($"{value}{_grid.GetValue(xFrom + 1, y)}", point);
            }

            value = _grid.GetValue(xTo - 1, y);
            if (value != ' ')
            {
                Point point = new(xTo - 1, y);
                points.Add($"{_grid.GetValue(xTo - 2, y)}{value}", point);
            }
        }

        _startPoint = ChangePointOuter(points["AA"][0]);
        _endPoint = ChangePointOuter(points["ZZ"][0]);

        foreach (var point in points.Where(x => x.Value.Count == 2))
        {
            var pointA = point.Value.ElementAt(0);
            var pointB = point.Value.ElementAt(1);

            _jumpPointsToOuter.Add(pointA, ChangePointInner(pointB));
            _jumpPointsToInner.Add(pointB, ChangePointOuter(pointA));
        }
    }

    private Point ChangePointOuter(Point point)
    {
        if (point.X == 1)
        {
            return point.Move(Direction.Right);
        }
        else if (point.X == _grid.SizeX - 2)
        {
            return point.Move(Direction.Left);
        }

        if (point.Y == 1)
        {
            return point.Move(Direction.Down);
        }
        else if (point.Y == _grid.SizeY - 2)
        {
            return point.Move(Direction.Up);
        }

        return point;
    }

    private static Point ChangePointInner(Point point)
    {
        if (point.X == xFrom)
        {
            return point.Move(Direction.Left);
        }
        else if (point.X == xTo - 1)
        {
            return point.Move(Direction.Right);
        }

        if (point.Y == yFrom)
        {
            return point.Move(Direction.Up);
        }
        else if (point.Y == yTo + 1)
        {
            return point.Move(Direction.Down);
        }

        return point;
    }

    private readonly struct DimensionPoint
    {
        public readonly int Dimension { get; private init; }
        public readonly Point Point { get; private init; }

        public DimensionPoint(int dimension, Point point)
        {
            Dimension = dimension; 
            Point = point; 
        }

        public DimensionPoint[] GetNeighbours()
        {
            var pointNeighbours = Point.GetNeighbours();
            DimensionPoint[] neighbours = new DimensionPoint[pointNeighbours.Length];

            for (int i = 0; i < pointNeighbours.Length; i++)
            {
                neighbours[i] = new(Dimension, pointNeighbours[i]);
            }

            return neighbours;
        }

        public override string ToString()
        {
            return $"{Dimension} {Point}";
        }
    }
}
