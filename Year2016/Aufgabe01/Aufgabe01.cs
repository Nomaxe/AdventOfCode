using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2016;

internal class Aufgabe01 : IAufgabe
{
    private readonly string[] _input;
    private Point _currentPosition;
    private Direction _direction;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2016, 1);
        _currentPosition = new();
        _direction = Direction.Up;
    }

    public string Calc()
    {
        var split = _input[0].Split(", ");
        foreach (var move in split)
        {
            SetNextDirection(move[0]);
            var number = int.Parse(move[1..]);
            _currentPosition = _currentPosition.Move(_direction, number);
        }

        return _currentPosition.GetManhattenDistanceToZero().ToString();
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
