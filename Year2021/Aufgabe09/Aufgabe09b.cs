using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe09b : IAufgabe
{
    private readonly GridInt _grid;

    public Aufgabe09b()
    {
        _grid = GridInt.CreateIntGrid(2021, 9);
    }

    public string Calc()
    {
        List<int> sizes = [];

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                var size = GetBasinSize(x, y);
                if (size > 0)
                {
                    sizes.Add(size);
                }
            }
        }

        return sizes.OrderDescending().Take(3).Aggregate(1, (x, y) => x * y).ToString();
    }

    private int GetBasinSize(int x, int y)
    {
        int height = _grid.GetValue(x, y);

        if (!_grid.GetInBoundNeighbours(x, y).All(x => _grid.GetValue(x) > height))
        {
            return 0;
        }

        Queue<Point> queue = new();
        queue.Enqueue(new(x, y));
        HashSet<Point> visited = [];

        while (queue.Count > 0)
        {
            var checkPoint = queue.Dequeue();
            visited.Add(checkPoint);
            height = _grid.GetValue(checkPoint);

            foreach (var neighbour in _grid.GetInBoundNeighbours(checkPoint).Where(x => !visited.Contains(x)))
            {
                var neighbourHeight = _grid.GetValue(neighbour);
                if (neighbourHeight > height && neighbourHeight != 9)
                {
                    queue.Enqueue(neighbour);
                }
            }
        }

        return visited.Count;
    }
}
