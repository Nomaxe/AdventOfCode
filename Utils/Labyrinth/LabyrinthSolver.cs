using System.Collections;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils.Labyrinth;

internal abstract class LabyrinthSolver

{
    protected Grid<char> _grid;
    protected Dictionary<Point, int> _length = [];
    protected HashSet<char> _wall;

    public LabyrinthSolver(Grid<char> grid)
    {
        _grid = grid;
        _wall = ['#'];
    }

    protected bool AddLength(Point point, int length)
    {
        ref var lengthRef = ref CollectionsMarshal.GetValueRefOrAddDefault(_length, point, out bool exists);
        if (!exists || length < lengthRef)
        {
            lengthRef = length;
            return true;
        }

        return false;
    }

    public void AddWallCharacter(char character)
    {
        _wall.Add(character);
    }

    public bool ContainsLength(Point point)
    {
        return _length.ContainsKey(point);
    }

    public int GetLength(Point point)
    {
        return _length[point];
    }

    public int GetMaxLength()
    {
        return _length.Values.Max();
    }

    public IEnumerable<int> GetLengthEnumerable()
    {
        return _length.Values;
    }

    public bool TryGetLength(Point point, out int length)
    {
        return _length.TryGetValue(point, out length);
    }

    public abstract void SolveLabyrinth(Point startPoint);
}
