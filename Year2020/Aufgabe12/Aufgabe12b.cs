using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2020;

internal class Aufgabe12b : IAufgabe
{
    private readonly string[] _input;
    private Point _ship;
    private Point _waypoint;

    public Aufgabe12b()
    {
        _input = Utilities.ReadInput(2020, 12);
        _ship = new(0, 0);
        _waypoint = new(10, -1);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            int number = int.Parse(line[1..]);

            switch (line[0])
            {
                case 'N':
                    _waypoint = _waypoint.MoveUp(number);
                    break;
                case 'S':
                    _waypoint = _waypoint.MoveDown(number);
                    break;
                case 'E':
                    _waypoint = _waypoint.MoveRight(number);
                    break;
                case 'W':
                    _waypoint = _waypoint.MoveLeft(number);
                    break;
                case 'L':
                    switch (number)
                    {
                        case 90:
                            _waypoint = _waypoint.RotateLeft();
                            break;
                        case 180:
                            _waypoint = _waypoint.Rotate180();
                            break;
                        case 270:
                            _waypoint = _waypoint.RotateRight();
                            break;
                    }
                    break;
                case 'R':
                    switch (number)
                    {
                        case 90:
                            _waypoint = _waypoint.RotateRight();
                            break;
                        case 180:
                            _waypoint = _waypoint.Rotate180();
                            break;
                        case 270:
                            _waypoint = _waypoint.RotateLeft();
                            break;
                    }
                    break;
                case 'F':
                    _ship = new(_ship.X + _waypoint.X * number, _ship.Y + _waypoint.Y * number);
                    break;
            }
        }

        return _ship.GetManhattenDistanceToZero().ToString();
    }
}
