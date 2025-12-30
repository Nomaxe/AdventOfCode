namespace AdventOfCode.Utils;

internal class PermuationGenerator<T>
{
    //ToDo non recursive für bessere Performance, da nicht so viele Iteratoren erstellt werden müssen
    private readonly List<T> _list;

    public PermuationGenerator(List<T> list)
    {
        _list = list;
    }

    public IEnumerable<List<T>> GetPermuations()
    {
        foreach (var item in GetPermuations(0, _list.Count - 1))
        {
            yield return item;
        }
    }

    private IEnumerable<List<T>> GetPermuations(int recursionDepth, int maxDepth)
    {
        if (recursionDepth == maxDepth)
        {
            yield return _list;
        }
        else
        {
            for (int i = recursionDepth; i <= maxDepth; i++)
            {
                Swap(recursionDepth, i);
                foreach (var item in GetPermuations(recursionDepth + 1, maxDepth))
                {
                    yield return item;
                }
                Swap(recursionDepth, i);
            }
        }
    }

    private void Swap(int a, int b)
    {
        if (a == b)
        {
            return;
        }

        (_list[b], _list[a]) = (_list[a], _list[b]);
    }
}
