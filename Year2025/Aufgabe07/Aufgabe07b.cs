using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2025;

internal class Aufgabe07b : IAufgabe
{
    private readonly Grid _grid;
    private readonly Queue<Point> _queue;
    private readonly LargeCounter<Point> _timelineCounter = new();
    private ulong _result;

    public Aufgabe07b()
    {
        _grid = Grid.CreateCharGrid(2025, 7);
        _queue = new();
    }

    public string Calc()
    {
        var startPoint = _grid.GetPointOfValue('S');
        _queue.Enqueue(startPoint);
        _timelineCounter.Add(startPoint);

        while (_queue.Count > 0)
        {
            MoveNextPoint();
        }

        return _result.ToString();
    }

    private void MoveNextPoint()
    {
        var point = _queue.Dequeue();
        var timelineCounter = _timelineCounter[point];

        //Die Split-Stellen sind immer 2 unter dem aktuellen Punkt
        point = point.MoveDown(2);

        if (point.Y >= _grid.SizeY)
        {
            _result += timelineCounter;
            return;
        }

        switch (_grid.GetValue(point))
        {
            case '^':
                var pointLeft = point.Move(Direction.Left);
                var pointRight = point.Move(Direction.Right);

                if (_timelineCounter.Add(pointLeft, timelineCounter))
                {
                    _queue.Enqueue(pointLeft);
                }
                if (_timelineCounter.Add(pointRight, timelineCounter))
                {
                    _queue.Enqueue(pointRight);
                }
                break;
            default:
                if (_timelineCounter.Add(point, timelineCounter))
                {
                    _queue.Enqueue(point);
                }
                break;
        }
    }
}
