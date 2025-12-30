namespace AdventOfCode.Utils.Comparer;

internal class IntDescendingComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return -x.CompareTo(y);
    }
}
