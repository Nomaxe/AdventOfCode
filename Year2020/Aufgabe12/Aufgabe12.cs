using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2020;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private Point _point;
    private Direction _direction;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2020, 12);
        _point = new(0, 0);
        _direction = Direction.Right;
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            int number = int.Parse(line[1..]);

            switch (line[0])
            {
                case 'N':
                    _point = _point.MoveUp(number);
                    break;
                case 'S':
                    _point = _point.MoveDown(number);
                    break;
                case 'E':
                    _point = _point.MoveRight(number);
                    break;
                case 'W':
                    _point = _point.MoveLeft(number);
                    break;
                case 'L':
                    switch (number)
                    {
                        case 90:
                            _direction = _direction.TurnLeft();
                            break;
                        case 180:
                            _direction = _direction.Reverse();
                            break;
                        case 270:
                            _direction = _direction.TurnRight();
                            break;
                    }
                    break;
                case 'R':
                    switch (number)
                    {
                        case 90:
                            _direction = _direction.TurnRight();
                            break;
                        case 180:
                            _direction = _direction.Reverse();
                            break;
                        case 270:
                            _direction = _direction.TurnLeft();
                            break;
                    }
                    break;
                case 'F':
                    _point = _point.Move(_direction, number);
                    break;
            }
        }

        return _point.GetManhattenDistanceToZero().ToString();
    }
}
