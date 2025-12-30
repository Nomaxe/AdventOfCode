namespace AdventOfCode.Utils;

internal static partial class ListExtensions
{
    internal static ulong GetDecimalNumber(this IList<bool> list)
    {
        ulong result = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i])
            {
                continue;
            }

            result += (ulong)Math.Pow(2, list.Count - i - 1);
        }

        return result;
    }

    internal static bool IsDistinct<T>(this List<T> list) where T : notnull
    {
        T value = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            if (!value.Equals(list[i]))
            {
                return false;
            }
        }

        return true;
    }
}
