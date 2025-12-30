using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe15b : IAufgabe
{
    private readonly GridInt _grid;
    private readonly GridInt _distance;

    public Aufgabe15b()
    {
        var grid = Grid.CreateIntGrid(2021, 15);
        _grid = new(grid.SizeX * 5, grid.SizeY * 5);

        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                var value = grid.GetValue(x, y);

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _grid.SetValue(x + grid.SizeX * i, y + grid.SizeY * j, GetValue(value, i + j));
                    }
                }
            }
        }

        _distance = new(_grid.SizeX, _grid.SizeY);
    }

    public string Calc()
    {
        Queue<Point> queue = new();
        queue.Enqueue(new(0, 0));

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();
            var currentDistance = _distance.GetValue(point);

            foreach (var neighbour in _distance.GetInBoundNeighbours(point))
            {
                var distanceToNeighbour = currentDistance + _grid.GetValue(neighbour);
                var currentNeighbourDistance = _distance.GetValue(neighbour);

                if (currentNeighbourDistance == 0 || distanceToNeighbour < currentNeighbourDistance)
                {
                    _distance.SetValue(neighbour, distanceToNeighbour);
                    queue.Enqueue(neighbour);
                }
            }
        }


        return _distance.GetValue(_distance.SizeX - 1, _distance.SizeY - 1).ToString();
    }

    private static int GetValue(int value, int plus)
    {
        value += plus;
        if (value > 9)
        {
            value -= 9;
        }

        return value;
    }
}
