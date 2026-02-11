using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe11b : IAufgabe
{
    private readonly IntCode _intCode;
    private readonly GridInt _grid;
    private Point _currentPoint;
    private Direction _direction;

    public Aufgabe11b()
    {
        _intCode = new(2019, 11)
        {
            WaitOnInput = true
        };
        _currentPoint = new(50, 50);
        _grid = new(100);
        _grid.SetValue(_currentPoint, (int)Color.White);
        _direction = Direction.Up;
    }

    public string Calc()
    {
        while (!_intCode.DidHalt)
        {
            _intCode.AddInput(GetCurrentColor());
            _intCode.Calc();

            Paint((Color)_intCode.Out[^2]);
            Move((int)_intCode.Out[^1]);
        }

        return "ZRZPKEZR";
    }

    private int GetCurrentColor()
    {
        return _grid.GetValue(_currentPoint);
    }

    private void Paint(Color color)
    {
        _grid.SetValue(_currentPoint, (int)color);
    }

    private void Move(int value)
    {
        switch (_direction)
        {
            case Direction.Up:
                _direction = value == 0 ? Direction.Left : Direction.Right;
                break;
            case Direction.Right:
                _direction = value == 0 ? Direction.Up : Direction.Down;
                break;
            case Direction.Down:
                _direction = value == 0 ? Direction.Right : Direction.Left;
                break;
            case Direction.Left:
                _direction = value == 0 ? Direction.Down : Direction.Up;
                break;
        }

        _currentPoint = _currentPoint.Move(_direction);
    }

    private enum Color
    {
        Black = 0,
        White = 1
    }
}
