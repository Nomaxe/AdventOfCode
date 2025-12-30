using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe09 : IAufgabe
{
    private readonly string[] _input;
    private Point _head;
    private Point _tail;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2022, 9);
        _head = new(0, 0);
        _tail = _head;
    }

    public string Calc()
    {
        HashSet<Point> visitedPoints = [_tail];

        foreach (var line in _input)
        {
            var direction = line[0].ToDirection();
            var number = int.Parse(line[2..]);

            for (int i = 0; i < number; i++)
            {
                _head = _head.Move(direction, 1);
                if (!_head.IsFullNeighbour(_tail))
                {
                    _tail = direction switch
                    {
                        Enums.Direction.Right => new(_head.X - 1, _head.Y),
                        Enums.Direction.Down => new(_head.X, _head.Y - 1),
                        Enums.Direction.Left => new(_head.X + 1, _head.Y),
                        Enums.Direction.Up => new(_head.X, _head.Y + 1),
                        _ => throw new NotImplementedException(),
                    };
                    visitedPoints.Add(_tail);
                }
            }
        }

        return visitedPoints.Count.ToString();
    }
}
