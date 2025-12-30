namespace AdventOfCode.Utils.Labyrinth;

internal interface ILabyrinthSolver : IEnumerable<Point>
{
    public void SolveLabyrinth(Point startPoint);
    public bool ContainsLength(Point point);
    public int GetLength(Point point);
    public bool TryGetLength(Point point, out int length);
}
