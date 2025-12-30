using AdventOfCode.Utils;
using System.Runtime.InteropServices;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2017;

internal class Aufgabe22 : IAufgabe
{
    private readonly Dictionary<Point, bool> _nodes;
    private Point _currentPosition;
    private Direction _direction;
    private int _bursts;

    public Aufgabe22()
    {
        var grid = Grid.CreateCharGrid(2017, 22);
        _nodes = new(grid.GridSize);
        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                _nodes.Add(new(x, y), grid.GetValue(x, y) == '#');
            }
        }
        _currentPosition = new(grid.SizeX / 2, grid.SizeY / 2);
        _direction = Direction.Up;
        _bursts = 0;
    }

    public string Calc()
    {
        for (int i = 0; i < 10000; i++)
        {
            Move();
        }

        return _bursts.ToString();
    }

    private void Move()
    {
        ref var currentValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_nodes, _currentPosition, out _);
        if (currentValue)
        {
            _direction = _direction.TurnRight();
        }
        else
        {
            _direction = _direction.TurnLeft();
            _bursts++;
        }

        currentValue = !currentValue;
        _currentPosition = _currentPosition.Move(_direction);
    }
}
