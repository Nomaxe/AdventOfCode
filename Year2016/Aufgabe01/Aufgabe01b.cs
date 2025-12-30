using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2016;

internal class Aufgabe01b : IAufgabe
{
    private readonly string[] _input;
    private Point _currentPosition;
    private Direction _direction;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInput(2016, 1);
        _currentPosition = new();
        _direction = Direction.Up;
    }

    public string Calc()
    {
        HashSet<Point> visitedPoints = [];
        var split = _input[0].Split(", ");
        foreach (var move in split)
        {
            SetNextDirection(move[0]);
            var number = int.Parse(move[1..]);
            for (int i = 1; i <= number; i++)
            {
                _currentPosition = _currentPosition.Move(_direction, 1);

                if (visitedPoints.Contains(_currentPosition))
                {
                    return _currentPosition.GetManhattenDistanceToZero().ToString();
                }

                visitedPoints.Add(_currentPosition);
            }

        }

        throw new NotImplementedException();
    }

    private void SetNextDirection(char move)
    {
        _direction = _direction switch
        {
            Direction.Right => move switch
            {
                'R' => Direction.Down,
                'L' => Direction.Up,
                _ => throw new NotImplementedException()
            },
            Direction.Down => move switch
            {
                'R' => Direction.Left,
                'L' => Direction.Right,
                _ => throw new NotImplementedException()
            },
            Direction.Left => move switch
            {
                'R' => Direction.Up,
                'L' => Direction.Down,
                _ => throw new NotImplementedException()
            },
            Direction.Up => move switch
            {
                'R' => Direction.Right,
                'L' => Direction.Left,
                _ => throw new NotImplementedException()
            },
            _ => throw new NotImplementedException(),
        };
    }
}
