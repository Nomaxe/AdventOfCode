using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe11b : IAufgabe
{
    private readonly GridInt _grid;

    public Aufgabe11b()
    {
        _grid = GridInt.CreateIntGrid(2021, 11);
    }

    public string Calc()
    {
        var gridSize = _grid.GridSize;

        for (int i = 1; true; i++)
        {
            HashSet<Point> flashingPoints = [];
            Queue<Point> queue = [];

            for (int y = 0; y < _grid.SizeY; y++)
            {
                for (int x = 0; x < _grid.SizeX; x++)
                {
                    var value = _grid.GetValue(x, y);
                    value++;

                    if (value >= 10)
                    {
                        Point point = new(x, y);
                        flashingPoints.Add(point);
                        queue.Enqueue(point);
                    }
                    else
                    {
                        _grid.SetValue(x, y, value);
                    }
                }
            }

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();

                foreach (var neighbour in _grid.GetInBoundFullNeighbours(point).Where(x => !flashingPoints.Contains(x)))
                {
                    var value = _grid.GetValue(neighbour);
                    value++;

                    if (value >= 10)
                    {
                        flashingPoints.Add(neighbour);
                        queue.Enqueue(neighbour);
                    }
                    else
                    {
                        _grid.SetValue(neighbour, value);
                    }
                }
            }

            if (flashingPoints.Count == gridSize)
            {
                return i.ToString();
            }

            foreach (var point in flashingPoints)
            {
                _grid.SetValue(point, 0);
            }
        }
    }
}
