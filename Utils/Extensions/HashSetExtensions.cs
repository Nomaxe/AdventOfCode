namespace AdventOfCode.Utils;

internal static partial class HashSetExtensions
{
    extension<T>(HashSet<T> set)
    {
        public void AddRange(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                set.Add(element);
            }
        }
    }
}
