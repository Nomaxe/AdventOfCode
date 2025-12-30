using AdventOfCode.Utils;
using System.Runtime.InteropServices;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2017;

internal class Aufgabe22b : IAufgabe
{
    private readonly Dictionary<Point, NodeState> _nodes;
    private Point _currentPosition;
    private Direction _direction;
    private int _bursts;

    public Aufgabe22b()
    {
        var grid = Grid.CreateCharGrid(2017, 22);
        _nodes = new(grid.GridSize);
        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                _nodes.Add(new(x, y), grid.GetValue(x, y) == '#' ? NodeState.Infected : NodeState.Clean);
            }
        }
        _currentPosition = new(grid.SizeX / 2, grid.SizeY / 2);
        _direction = Direction.Up;
        _bursts = 0;
    }

    public string Calc()
    {
        for (int i = 0; i < 10000000; i++)
        {
            Move();
        }

        return _bursts.ToString();
    }

    private void Move()
    {
        ref var currentValue = ref CollectionsMarshal.GetValueRefOrAddDefault(_nodes, _currentPosition, out _);
        switch (currentValue)
        {
            case NodeState.Clean:
                _direction = _direction.TurnLeft();
                currentValue = NodeState.Weakened;
                break;
            case NodeState.Weakened:
                currentValue = NodeState.Infected;
                _bursts++;
                break;
            case NodeState.Infected:
                _direction = _direction.TurnRight();
                currentValue = NodeState.Flagged;
                break;
            case NodeState.Flagged:
                _direction = _direction.Reverse();
                currentValue = NodeState.Clean;
                break;
        }

        _currentPosition = _currentPosition.Move(_direction);
    }

    private enum NodeState
    {
        Clean,
        Weakened,
        Infected,
        Flagged
    }
}
