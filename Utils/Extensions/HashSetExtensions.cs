namespace AdventOfCode.Utils;

internal static class HashSetExtensions
{
    public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> elements)
    {
        foreach (var element in elements)
        {
            set.Add(element);
        }
    }
}
