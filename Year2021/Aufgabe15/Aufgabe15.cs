using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe15 : IAufgabe
{
    private readonly GridInt _grid;
    private readonly GridInt _distance;

    public Aufgabe15()
    {
        _grid = Grid.CreateIntGrid(2021, 15);
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
}
