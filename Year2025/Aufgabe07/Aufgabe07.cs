using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2025;

internal class Aufgabe07 : IAufgabe
{
    private readonly Grid _grid;
    private readonly Queue<Point> _queue;
    private int _splitCount = 0;

    public Aufgabe07()
    {
        _grid = Grid.CreateCharGrid(2025, 7);
        _queue = new();
    }

    public string Calc()
    {
        _queue.Enqueue(_grid.GetPointOfValue('S'));

        while (_queue.Count > 0)
        {
            MoveNextPoint();
        }

        return _splitCount.ToString();
    }

    private void MoveNextPoint()
    {
        var point = _queue.Dequeue();

        do
        {
            var currentValue = _grid.GetValue(point);

            switch (currentValue)
            {
                case '|':
                    return;
                case '^':
                    _splitCount++;
                    _queue.Enqueue(point.Move(Direction.Left));
                    _queue.Enqueue(point.Move(Direction.Right));
                    return;
                default:
                    _grid.SetValue(point, '|');
                    point = point.Move(Direction.Down);
                    break;
            }

            if (point.Y == _grid.SizeY)
            {
                return;
            }
        } while (true);
    }
}
