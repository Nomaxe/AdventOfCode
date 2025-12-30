using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;
using AdventOfCode.Year2019.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe15 : IAufgabe
{
    private readonly IntCode _intcode;
    private readonly Grid _grid;
    private Point _position;

    public Aufgabe15()
    {
        _intcode = new(2019, 15)
        {
            WaitOnInput = true
        };
        _grid = new(100);
        _position = new(49, 49);
    }

    public string Calc()
    {
        Point oxygenSystem = new();

        while (true)
        {
            if (!GetDirection(out Direction direction, out Point checkPoint))
            {
                break;
            }
            _intcode.AddInput(GetIntCodeDirection(direction));
            _intcode.Calc();
            var output = _intcode.Out[^1];
            switch (output)
            {
                case 0:
                    _grid.SetValue(checkPoint, '#');
                    break;
                case 1:
                    _grid.SetValue(checkPoint, '.');
                    _position = checkPoint;
                    break;
                case 2:
                    _grid.SetValue(checkPoint, '.');
                    _position = checkPoint;
                    oxygenSystem = checkPoint;
                    break;
            }
        }

        CompleteSolver solver = new(_grid);
        solver.SolveLabyrinth(new(49, 49));
        return solver.GetLength(oxygenSystem).ToString();
    }

    private bool GetDirection(out Direction direction, out Point checkPoint)
    {
        if (_grid.GetValue(_position.X, _position.Y - 1) == '\0')
        {
            direction = Direction.Up;
            checkPoint = new(_position.X, _position.Y - 1);
            return true;
        }
        if (_grid.GetValue(_position.X + 1, _position.Y) == '\0')
        {
            direction = Direction.Right;
            checkPoint = new(_position.X + 1, _position.Y);
            return true;
        }
        if (_grid.GetValue(_position.X, _position.Y + 1) == '\0')
        {
            direction = Direction.Down;
            checkPoint = new(_position.X, _position.Y + 1);
            return true;
        }
        if (_grid.GetValue(_position.X - 1, _position.Y) == '\0')
        {
            direction = Direction.Left;
            checkPoint = new(_position.X - 1, _position.Y);
            return true;
        }

        _grid.SetValue(_position, '_');

        if (_grid.GetValue(_position.X, _position.Y - 1) == '.')
        {
            direction = Direction.Up;
            checkPoint = new(_position.X, _position.Y - 1);
            return true;
        }
        if (_grid.GetValue(_position.X + 1, _position.Y) == '.')
        {
            direction = Direction.Right;
            checkPoint = new(_position.X + 1, _position.Y);
            return true;
        }
        if (_grid.GetValue(_position.X, _position.Y + 1) == '.')
        {
            direction = Direction.Down;
            checkPoint = new(_position.X, _position.Y + 1);
            return true;
        }
        if (_grid.GetValue(_position.X - 1, _position.Y) == '.')
        {
            direction = Direction.Left;
            checkPoint = new(_position.X - 1, _position.Y);
            return true;
        }

        direction = Direction.Down;
        checkPoint = new();
        return false;
    }

    private static int GetIntCodeDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Right => 4,
            Direction.Down => 2,
            Direction.Left => 3,
            Direction.Up => 1,
            _ => throw new NotImplementedException(),
        };
    }
}
