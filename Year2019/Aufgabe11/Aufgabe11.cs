using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;
using System.Runtime.InteropServices;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe11 : IAufgabe
{
    private readonly IntCode _intCode;
    private readonly Dictionary<Point, Color> _panels;
    private Point _currentPoint;
    private Direction _direction;

    public Aufgabe11()
    {
        _intCode = new(2019, 11)
        {
            WaitOnInput = true
        };
        _panels = [];
        _currentPoint = new(0, 0);
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

        return _panels.Count.ToString();
    }

    private int GetCurrentColor()
    {
        if (_panels.TryGetValue(_currentPoint, out var color))
        {
            return (int)color;
        }

        return 0;
    }

    private void Paint(Color color)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(_panels, _currentPoint, out _);
        value = color;
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
