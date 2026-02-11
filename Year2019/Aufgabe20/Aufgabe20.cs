using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2019;

internal class Aufgabe20 : IAufgabe
{
    private readonly Grid _grid;
    private readonly CustomCompleteSolver _solver;
    private readonly Dictionary<Point, Point> _jumpPoints = [];
    private Point _startPoint;
    private Point _endPoint;

    public Aufgabe20()
    {
        _grid = Grid.CreateCharGrid(2019, 20);
        _solver = new(_grid, this);
    }

    public string Calc()
    {
        for (int i = 'A'; i < 'Z'; i++)
        {
            _solver.AddWallCharacter((char)i);
        }

        GetJumpPoints();
        _solver.SolveLabyrinth(_startPoint);
        return _solver.GetLength(_endPoint).ToString();
    }

    private void GetJumpPoints()
    {
        DictionaryHashSet<string, Point> points = [];
        Dictionary<Point, Point> endPoints = [];

        //oben & unten
        for (int x = 0; x < _grid.SizeX; x++)
        {
            var value = _grid.GetValue(x, 0);
            if (value != ' ')
            {
                Point point = new(x, 1);
                points.Add($"{value}{_grid.GetValue(x, 1)}", point);
                endPoints.Add(point, new(x, 2));
            }

            value = _grid.GetValue(x, _grid.SizeY - 1);
            if (value != ' ')
            {
                Point point = new(x, _grid.SizeY - 2);
                points.Add($"{_grid.GetValue(x, _grid.SizeY - 2)}{value}", point);
                endPoints.Add(point, new(x, _grid.SizeY - 3));
            }
        }

        //links & rechts
        for (int y = 0; y < _grid.SizeY; y++)
        {
            var value = _grid.GetValue(0, y);
            if (value != ' ')
            {
                Point point = new(1, y);
                points.Add($"{value}{_grid.GetValue(1, y)}", point);
                endPoints.Add(point, new(2, y));
            }

            value = _grid.GetValue(_grid.SizeX - 1, y);
            if (value != ' ')
            {
                Point point = new(_grid.SizeX - 2, y);
                points.Add($"{_grid.GetValue(_grid.SizeX - 2, y)}{value}", point);
                endPoints.Add(point, new(_grid.SizeX - 3, y));
            }
        }

        //Donut innen - we assume, that the donut is always the same size
        //oben & unten
        for (int x = 33; x < 90; x++)
        {
            var value = _grid.GetValue(x, 33);
            if (value != ' ')
            {
                Point point = new(x, 33);
                points.Add($"{value}{_grid.GetValue(x, 34)}", point);
                endPoints.Add(point, new(x, 32));
            }

            value = _grid.GetValue(x, 91);
            if (value != ' ')
            {
                Point point = new(x, 91);
                points.Add($"{_grid.GetValue(x, 90)}{value}", point);
                endPoints.Add(point, new(x, 92));
            }
        }

        //links & rechts
        for (int y = 33; y < 90; y++)
        {
            var value = _grid.GetValue(33, y);
            if (value != ' ')
            {
                Point point = new(33, y);
                points.Add($"{value}{_grid.GetValue(34, y)}", point);
                endPoints.Add(point, new(32, y));
            }

            value = _grid.GetValue(89, y);
            if (value != ' ')
            {
                Point point = new(89, y);
                points.Add($"{_grid.GetValue(88, y)}{value}", point);
                endPoints.Add(point, new(90, y));
            }
        }

        _startPoint = endPoints[points["AA"].First()];
        _endPoint = endPoints[points["ZZ"].First()];

        foreach (var point in points.Where(x => x.Value.Count == 2))
        {
            var pointA = point.Value.ElementAt(0);
            var pointB = point.Value.ElementAt(1);

            _jumpPoints.Add(pointA, endPoints[pointB]);
            _jumpPoints.Add(pointB, endPoints[pointA]);
        }
    }

    private class CustomCompleteSolver : CompleteSolver
    {
        private readonly Aufgabe20 _aufgabe;

        public CustomCompleteSolver(Grid<char> grid, Aufgabe20 aufgabe) : base(grid)
        {
            _aufgabe = aufgabe;
        }

        protected override IEnumerable<Point> GetNeighbours(Point point)
        {
            foreach (var neighbour in base.GetNeighbours(point))
            {
                if (_aufgabe._jumpPoints.TryGetValue(neighbour, out var jumpPoint))
                {
                    yield return jumpPoint;
                }
                else
                {
                    yield return neighbour;
                }
            }
        }
    }
}
